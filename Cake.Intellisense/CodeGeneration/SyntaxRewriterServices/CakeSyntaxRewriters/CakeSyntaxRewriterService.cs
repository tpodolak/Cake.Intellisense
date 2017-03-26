using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.CakeSyntaxRewriters.Interfaces;
using Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.Interfaces;
using Cake.Intellisense.Compilation.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NLog;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.CakeSyntaxRewriters
{
    public class CakeSyntaxRewriterService : ICakeSyntaxRewriterService
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly ICompilationProvider _compilationProvider;
        private readonly IEnumerable<ISyntaxRewriterService> _metadataRewriterServices;

        public CakeSyntaxRewriterService(ICompilationProvider compilationProvider, IEnumerable<ISyntaxRewriterService> metadataRewriterServices)
        {
            _compilationProvider = compilationProvider ?? throw new ArgumentNullException(nameof(compilationProvider));
            _metadataRewriterServices = metadataRewriterServices?.OrderBy(service => service.Order) ?? throw new ArgumentNullException(nameof(metadataRewriterServices));
        }

        public SyntaxNode Rewrite(CompilationUnitSyntax compilationUnitSyntax, Assembly assembly)
        {
            var compilation = _compilationProvider.Get(assembly.GetName().Name);

            compilation = compilation.AddSyntaxTrees(CSharpSyntaxTree.Create(compilationUnitSyntax));

            foreach (var metadataRewriterService in _metadataRewriterServices)
            {
                var rewriterName = metadataRewriterService.GetType().Name;
                Logger.Info($"Executing {rewriterName}");
                var currentTree = compilation.SyntaxTrees.Single();
                var semanticModel = compilation.GetSemanticModel(currentTree);
                var rewrittenNode = metadataRewriterService.Rewrite(assembly, semanticModel, currentTree.GetRoot());
                compilation = compilation.ReplaceSyntaxTree(currentTree, SyntaxTree(rewrittenNode));
                Logger.Info($"Finished execution of {rewriterName}");
            }

            return compilation.SyntaxTrees.Single().GetRoot();
        }
    }
}