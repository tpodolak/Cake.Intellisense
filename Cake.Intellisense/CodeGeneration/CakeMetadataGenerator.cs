using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cake.Core.Scripting;
using Cake.MetadataGenerator.CodeGeneration.MetadataRewriterServices;
using Cake.MetadataGenerator.CodeGeneration.MetadataRewriterServices.ClassRewriters;
using Cake.MetadataGenerator.CodeGeneration.MetadataRewriterServices.CommentRewriters;
using Cake.MetadataGenerator.CodeGeneration.MetadataRewriterServices.MethodRewriters;
using Cake.MetadataGenerator.Documentation;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Cake.MetadataGenerator.CodeGeneration
{
    public class CakeMetadataGenerator : ICakeMetadataGenerator
    {
        private readonly IEnumerable<IMetadataRewriterService> _metadataRewriterServices;

        public CakeMetadataGenerator(IEnumerable<IMetadataRewriterService> metadataRewriterServices)
        {
            _metadataRewriterServices = metadataRewriterServices.OrderBy(service => service.Order).ToList();
        }

        public SyntaxTree Generate(Assembly assembly)
        {
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName: "temp",
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
            var semanticModel = compilation.GetSemanticModel(tree);
            var commentsRewriter = new CommentSyntaxRewriter(new DocumentationReader(), new XmlCommentProvider(), semanticModel);
            var resxxxx = commentsRewriter.Visit(assemblies.Single(), tree.GetRoot());
            compilation = compilation.ReplaceSyntaxTree(tree, SyntaxTree(resxxxx));
            tree = compilation.SyntaxTrees.FirstOrDefault();
            var rewriter = new ClassSyntaxRewriter();
            var result = rewriter.Visit(tree.GetRoot());
            compilation = compilation.ReplaceSyntaxTree(tree, SyntaxTree(result));

            tree = compilation.SyntaxTrees.First();


            semanticModel = compilation.GetSemanticModel(tree);
            var methodRewriter = new MethodSyntaxRewriter(semanticModel);
            result = methodRewriter.Visit(tree.GetRoot());

            compilation = compilation.ReplaceSyntaxTree(tree, SyntaxTree(result));

            tree = compilation.SyntaxTrees.First();
            var attributeRewriter = new AttributesSyntaxRewriter();
            result = attributeRewriter.Visit(tree.GetRoot());

            var diagnostics = result.GetDiagnostics().ToList();

            compilation = compilation.ReplaceSyntaxTree(tree, SyntaxTree(result));

            compilation = compilation.AddReferences(referencesass);
        }


        private IEnumerable<ClassDeclarationSyntax> CreateNamedTypeDeclaration(INamespaceOrTypeSymbol namepace)
        {
            return namepace.GetTypeMembers()
                .Where(val => val.Kind == SymbolKind.NamedType && (val.Name == typeof(ScriptHost).Name || val.GetAttributes().Any(x => x.AttributeClass.Name == "CakeAliasCategoryAttribute")))
                .Select(val => _codeGenerationService.CreateNamedTypeDeclaration(val));
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

    public interface ICakeMetadataGenerator
    {

    }
}