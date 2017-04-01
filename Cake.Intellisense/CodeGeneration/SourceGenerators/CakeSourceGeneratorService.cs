using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cake.Intellisense.CodeGeneration.SourceGenerators.Interfaces;
using Cake.Intellisense.Compilation.Interfaces;
using Cake.Intellisense.Reflection.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Cake.Intellisense.Constants.CakeAttributeNames;
using static Cake.Intellisense.Constants.CakeEngineNames;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Cake.Intellisense.CodeGeneration.SourceGenerators
{
    public class CakeSourceGeneratorService : ICakeSourceGeneratorService
    {
        private readonly IMetadataGeneratorService _metadataGeneratorService;
        private readonly IMetadataReferenceLoader _metadataReferenceLoader;
        private readonly ICompilationProvider _compilationProvider;

        public CakeSourceGeneratorService(
            IMetadataGeneratorService metadataGeneratorService,
            IMetadataReferenceLoader metadataReferenceLoader,
            ICompilationProvider compilationProvider)
        {
            _metadataGeneratorService = metadataGeneratorService ?? throw new ArgumentNullException(nameof(metadataGeneratorService));
            _metadataReferenceLoader = metadataReferenceLoader ?? throw new ArgumentNullException(nameof(metadataReferenceLoader));
            _compilationProvider = compilationProvider ?? throw new ArgumentNullException(nameof(compilationProvider));
        }

        public CompilationUnitSyntax Generate(Assembly assembly)
        {
            var assemblyName = assembly.GetName();
            var assemblyVersion = assemblyName.Version;
            var compilation = _compilationProvider.Get(
                assembly.GetName().Name,
                references: new[] { _metadataReferenceLoader.CreateFromFile(assembly.Location) });

            var namespaceSymbols = GetNamespaceMembers(compilation.GlobalNamespace);

            var compilationSyntax = CompilationUnit();

            compilationSyntax = compilationSyntax.AddUsings(UsingDirective(IdentifierName("System.Reflection")))
                    .WithAttributeLists(List(new[]
                    {
                        CreateAssemblyAttributeList("AssemblyTitle", $"{assemblyName.Name}"),
                        CreateAssemblyAttributeList("AssemblyVersion", $"{assemblyVersion}"),
                        CreateAssemblyAttributeList("AssemblyFileVersion", $"{assemblyVersion}"),
                        CreateAssemblyAttributeList("AssemblyInformationalVersion", $"{assemblyVersion}")
                    }));

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
                .Where(val => val.Kind == SymbolKind.NamedType && (val.Name == ScriptHostName || val.GetAttributes().Any(x => x.AttributeClass.Name == CakeAliasCategoryName)))
                .Select(val => _metadataGeneratorService.CreateNamedTypeDeclaration(val));
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

        private AttributeListSyntax CreateAssemblyAttributeList(string attributeName, string attributeValue)
        {
            return AttributeList(
                    SingletonSeparatedList(
                        Attribute(
                                IdentifierName(attributeName))
                            .WithArgumentList(
                                AttributeArgumentList(
                                    SingletonSeparatedList(
                                        AttributeArgument(
                                            LiteralExpression(
                                                SyntaxKind.StringLiteralExpression,
                                                Literal(attributeValue))))))))
                .WithTarget(AttributeTargetSpecifier(Token(SyntaxKind.AssemblyKeyword)));
        }
    }
}