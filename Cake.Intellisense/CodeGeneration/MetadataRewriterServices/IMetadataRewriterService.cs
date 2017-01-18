using System.Reflection;
using Microsoft.CodeAnalysis;

namespace Cake.MetadataGenerator.CodeGeneration.MetadataRewriterServices
{
    public interface IMetadataRewriterService
    {
        int Order { get; }

        SyntaxNode Rewrite(Assembly assemlby, SemanticModel semanticModel, SyntaxNode node);
    }
}