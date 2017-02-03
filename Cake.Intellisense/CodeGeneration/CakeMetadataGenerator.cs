using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cake.MetadataGenerator.CodeGeneration.MetadataGenerators;
using Cake.MetadataGenerator.CodeGeneration.MetadataRewriterServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Cake.MetadataGenerator.CodeGeneration
{
    public class CakeMetadataGenerator : ICakeMetadataGenerator
    {
        private readonly IMetadataGeneratorService metadataGeneratorService;
        private readonly IEnumerable<IMetadataRewriterService> metadataRewriterServices;

        public CakeMetadataGenerator(IMetadataGeneratorService metadataGeneratorService, IEnumerable<IMetadataRewriterService> metadataRewriterServices)
        {
            this.metadataGeneratorService = metadataGeneratorService;
            this.metadataRewriterServices = metadataRewriterServices.OrderBy(service => service.Order).ToList();
        }

        public SyntaxTree Generate(Assembly assembly)
        {
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName: assembly.GetName().Name,
                references: new[] { MetadataReference.CreateFromFile(assembly.Location) });


            var namespaceSymbols = GetNamespaceMembers(compilation.GlobalNamespace).ToList();

            var compilationSyntax = CompilationUnit();

            foreach (var namespaceSymbol in namespaceSymbols)
            {
                var classSymbols = CreateNamedTypeDeclaration(namespaceSymbol).ToArray();
                if (classSymbols.Any())
                {
                    var namespaceSyntax = CreateNamespace(namespaceSymbol.ToString()).AddMembers(classSymbols);
                    compilationSyntax = compilationSyntax.AddMembers(namespaceSyntax);
                }
            }

            var tree = CSharpSyntaxTree.Create(compilationSyntax);
            compilation = compilation.AddSyntaxTrees(tree);

            foreach (var metadataRewriterService in metadataRewriterServices)
            {
                var currentTree = compilation.SyntaxTrees.Single();
                var semanticModel = compilation.GetSemanticModel(currentTree);
                var rewrittenNode = metadataRewriterService.Rewrite(assembly, semanticModel, currentTree.GetRoot());
                compilation = compilation.ReplaceSyntaxTree(currentTree, SyntaxTree(rewrittenNode));
            }

            return compilation.SyntaxTrees.Single();
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

        public NamespaceDeclarationSyntax CreateNamespace(string @namespace)
        {
            return NamespaceDeclaration(IdentifierName(@namespace));
        }
    }
}