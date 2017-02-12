using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
namespace Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices.CakeSyntaxRewriters
{
    public class CakeSyntaxRewriterService : ICakeSyntaxRewriterService
    {
        private readonly IEnumerable<ISyntaxRewriterService> metadataRewriterServices;

        public CakeSyntaxRewriterService(IEnumerable<ISyntaxRewriterService> metadataRewriterServices)
        {
            this.metadataRewriterServices = metadataRewriterServices.OrderBy(service => service.Order).ToList();
        }

        public SyntaxNode Rewrite(CompilationUnitSyntax compilationUnitSyntax, Assembly assembly)
        {
            var compilation = CSharpCompilation.Create(assembly.GetName().Name);

            compilation = compilation.AddSyntaxTrees(CSharpSyntaxTree.Create(compilationUnitSyntax));

            foreach (var metadataRewriterService in metadataRewriterServices)
            {
                var currentTree = compilation.SyntaxTrees.Single();
                var semanticModel = compilation.GetSemanticModel(currentTree);
                var rewrittenNode = metadataRewriterService.Rewrite(assembly, semanticModel, currentTree.GetRoot());
                compilation = compilation.ReplaceSyntaxTree(currentTree, SyntaxTree(rewrittenNode));
            }

            return compilation.SyntaxTrees.Single().GetRoot();
        }
    }
}