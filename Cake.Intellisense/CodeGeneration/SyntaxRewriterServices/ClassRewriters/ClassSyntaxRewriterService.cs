using System.Reflection;
using Microsoft.CodeAnalysis;

namespace Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices.ClassRewriters
{
    public class ClassSyntaxRewriterService : ISyntaxRewriterService
    {
        public int Order { get; } = 1;

        public SyntaxNode Rewrite(Assembly assembly, SemanticModel semanticModel, SyntaxNode node)
        {
            var classRewriter = new ClassSyntaxRewriter(MetadataGeneration.MetadataClassSuffix);
            return classRewriter.Visit(node).NormalizeWhitespace();
        }
    }
}