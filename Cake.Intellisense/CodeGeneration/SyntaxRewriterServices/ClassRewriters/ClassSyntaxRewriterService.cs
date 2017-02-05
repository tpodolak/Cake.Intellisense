using System.Reflection;
using Microsoft.CodeAnalysis;

namespace Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices.ClassRewriters
{
    public class ClassSyntaxRewriterService : ISyntaxRewriterService
    {
        public int Order { get; } = 1;

        public SyntaxNode Rewrite(Assembly assemlby, SemanticModel semanticModel, SyntaxNode node)
        {
            var classRewriter = new ClassSyntaxRewriter(MetadataGeneration.MetadataClassSufix);
            return classRewriter.Visit(node).NormalizeWhitespace();
        }
    }
}