using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Cake.MetadataGenerator.Tests.Unit.SyntaxRewriterTests
{
    public class AttributeRemoverSyntaxRewriterTests
    {
        private string code = @"
[global::Cake.Core.Annotations.CakeAliasCategoryAttribute(null)]
public static class ArgumentAliases : global::System.Object
{
    public static string MyProp { get; set; }

    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static T Argument<T>(this global::Cake.Core.ICakeContext context, System.String name)
    {
    }

    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static T Argument<T>(this global::Cake.Core.ICakeContext context, System.String name, T defaultValue)
    {
    }

    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static System.Boolean HasArgument(this global::Cake.Core.ICakeContext context, System.String name)
    {
    }
}";

        [Fact]
        public void Foo()
        {
            var syntax = ParseSyntaxTree(code);
            var rewriter = new CakeAttributesRemoverSyntaxRewriter();
            var result = rewriter.Visit(syntax.GetRoot());
            var stringResult = result.NormalizeWhitespace().ToFullString();
        }

    }
}