﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cake.Intellisense.Compilation;
using Cake.Intellisense.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cake.Intellisense.CodeGeneration.SourceGenerators
{
    public class CakeSourceGeneratorService : ICakeSourceGeneratorService
    {
        private readonly IMetadataGeneratorService metadataGeneratorService;
        private readonly IMetadataReferenceLoader metadataReferenceLoader;
        private readonly ICompilationProvider compilationProvider;

        public CakeSourceGeneratorService(
            IMetadataGeneratorService metadataGeneratorService,
            IMetadataReferenceLoader metadataReferenceLoader,
            ICompilationProvider compilationProvider)
        {
            this.metadataGeneratorService = metadataGeneratorService;
            this.metadataReferenceLoader = metadataReferenceLoader;
            this.compilationProvider = compilationProvider;
        }

        public CompilationUnitSyntax Generate(Assembly assembly)
        {
            var compilation = compilationProvider.Get(
                assembly.GetName().Name,
                references: new[] { metadataReferenceLoader.CreateFromFile(assembly.Location) });

            var namespaceSymbols = GetNamespaceMembers(compilation.GlobalNamespace);

            var compilationSyntax = SyntaxFactory.CompilationUnit();

            foreach (var namespaceSymbol in namespaceSymbols)
            {
                var classSymbols = CreateNamedTypeDeclaration(namespaceSymbol).ToArray();
                if (classSymbols.Any())
                {
                    var namespaceSyntax = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName(namespaceSymbol.ToString())).AddMembers(classSymbols);
                    compilationSyntax = compilationSyntax.AddMembers(namespaceSyntax);
                }
            }

            return compilationSyntax;
        }

        private IEnumerable<ClassDeclarationSyntax> CreateNamedTypeDeclaration(INamespaceOrTypeSymbol namepace)
        {
            return namepace.GetTypeMembers()
                .Where(val => val.Kind == SymbolKind.NamedType && (val.Name == CakeEngineNames.ScriptHost || val.GetAttributes().Any(x => x.AttributeClass.Name == CakeAttributeNames.CakeAliasCategory)))
                .Select(val => metadataGeneratorService.CreateNamedTypeDeclaration(val));
        }

        private IEnumerable<INamespaceOrTypeSymbol> GetNamespaceMembers(INamespaceSymbol symbol)
        {
            yield return symbol;

            foreach (var innerSymbol in symbol.GetNamespaceMembers())
            {
                foreach (var reccured in GetNamespaceMembers(innerSymbol))
                {
                    yield return reccured;
                }
            }
        }
    }
}