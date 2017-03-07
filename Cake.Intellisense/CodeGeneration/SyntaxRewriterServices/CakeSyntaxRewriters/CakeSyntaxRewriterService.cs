using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cake.Intellisense.Compilation;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.CakeSyntaxRewriters
{
    public class CakeSyntaxRewriterService : ICakeSyntaxRewriterService
    {
        private readonly ICompilationProvider compilationProvider;
        private readonly IEnumerable<ISyntaxRewriterService> metadataRewriterServices;

        public CakeSyntaxRewriterService(ICompilationProvider compilationProvider, IEnumerable<ISyntaxRewriterService> metadataRewriterServices)
        {
            this.compilationProvider = compilationProvider;
            this.metadataRewriterServices = metadataRewriterServices.OrderBy(service => service.Order);
        }

        public SyntaxNode Rewrite(CompilationUnitSyntax compilationUnitSyntax, Assembly assembly)
        {
            var compilation = compilationProvider.Get(assembly.GetName().Name);

            compilation = compilation.AddSyntaxTrees(CSharpSyntaxTree.Create(compilationUnitSyntax));

            foreach (var metadataRewriterService in metadataRewriterServices)
            {
                var currentTree = compilation.SyntaxTrees.Single();
                var semanticModel = compilation.GetSemanticModel(currentTree);
                var rewrittenNode = metadataRewriterService.Rewrite(assembly, semanticModel, currentTree.GetRoot());
                compilation = compilation.ReplaceSyntaxTree(currentTree, SyntaxFactory.SyntaxTree(rewrittenNode));
            }

            return compilation.SyntaxTrees.Single().GetRoot();
        }
    }
}