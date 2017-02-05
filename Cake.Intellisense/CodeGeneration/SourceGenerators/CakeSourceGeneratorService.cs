using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cake.MetadataGenerator.CodeGeneration.MetadataGenerators;
using Cake.MetadataGenerator.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Cake.MetadataGenerator.CodeGeneration.SourceGenerators
{
    public class CakeSourceGeneratorService : ICakeSourceGeneratorService
    {
        private readonly IMetadataGeneratorService metadataGeneratorService;
        private readonly IMetadataReferenceLoader metadataReferenceLoader;

        public CakeSourceGeneratorService(IMetadataGeneratorService metadataGeneratorService, IMetadataReferenceLoader metadataReferenceLoader)
        {
            this.metadataGeneratorService = metadataGeneratorService;
            this.metadataReferenceLoader = metadataReferenceLoader;
        }

        public CompilationUnitSyntax Generate(Assembly assembly)
        {
            var compilation = CSharpCompilation.Create(
                assemblyName: assembly.GetName().Name,
                references: new[] { metadataReferenceLoader.CreateFromFile(assembly.Location) });


            var namespaceSymbols = GetNamespaceMembers(compilation.GlobalNamespace).ToList();

            var compilationSyntax = CompilationUnit();

            foreach (var namespaceSymbol in namespaceSymbols)
            {
                var classSymbols = CreateNamedTypeDeclaration(namespaceSymbol).ToArray();
                if (classSymbols.Any())
                {
                    var namespaceSyntax = NamespaceDeclaration(IdentifierName(namespaceSymbol.ToString())).AddMembers(classSymbols);
                    compilationSyntax = compilationSyntax.AddMembers(namespaceSyntax);
                }
            }

            return compilationSyntax;
        }

        private IEnumerable<ClassDeclarationSyntax> CreateNamedTypeDeclaration(INamespaceOrTypeSymbol namepace)
        {
            return namepace.GetTypeMembers()
                .Where(val => val.Kind == SymbolKind.NamedType && (val.Name == "ScriptHost" || val.GetAttributes().Any(x => x.AttributeClass.Name == "CakeAliasCategoryAttribute")))
                .Select(val => metadataGeneratorService.CreateNamedTypeDeclaration(val));
        }

        private static IEnumerable<INamespaceOrTypeSymbol> GetNamespaceMembers(INamespaceSymbol symbol)
        {
            yield return symbol;
            if (symbol.GetNamespaceMembers().Any())
            {
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
}