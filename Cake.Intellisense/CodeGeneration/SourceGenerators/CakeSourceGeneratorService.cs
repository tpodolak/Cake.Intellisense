using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cake.Intellisense.Compilation;
using Cake.Intellisense.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

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
            var assemblyName = assembly.GetName();
            var assemblyVersion = assemblyName.Version;
            var compilation = compilationProvider.Get(
                assembly.GetName().Name,
                references: new[] { metadataReferenceLoader.CreateFromFile(assembly.Location) });

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