using System.Linq;
using System.Xml.Linq;
using Cake.MetadataGenerator.CodeGeneration.MetadataRewriterServices.MethodRewriters;
using Cake.MetadataGenerator.Documentation;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NSubstitute;
using Xunit;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Cake.MetadataGenerator.Tests.Unit.SyntaxRewriterTests
{
    public class AttributeRemoverSyntaxRewriterTests
    {
        private string code = @"
[global::Cake.Core.Annotations.CakeAliasCategoryAttribute(null)]
public static class ArgumentAliases
{
    public static string MyProp { get; set; }

    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static T Argument<T>(this global::Cake.Core.ICakeContext context, out System.String name)
    {
    }

    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static T Argument<T>(this global::Cake.Core.ICakeContext context, System.String name, out T defaultValue)
    {
    }

    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static System.Boolean HasArgument(this global::Cake.Core.ICakeContext context, out System.String name)
    {
    }

    public System.Boolean HasArgument2(this global::Cake.Core.ICakeContext context, out System.String name)
    {
    }

    private static System.Boolean HasArgument2(this global::Cake.Core.ICakeContext context, out System.String name)
    {
    }

    internal static System.Boolean HasArgument23(this global::Cake.Core.ICakeContext context, out System.String name)
    {
    }
}";

        [Fact]
        public void Foo()
        {
            var syntax = ParseSyntaxTree(code);

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName: "name",
                syntaxTrees: new[] { syntax },
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            var syntaxTree = compilation.SyntaxTrees.First();
            var semanticModel = compilation.GetSemanticModel(syntaxTree);
            // var rewriter = new CakeAttributeRemover(semanticModel);

            var provider = Substitute.For<ICommentProvider>();
            provider.Get(Arg.Any<XDocument>(), Arg.Any<ISymbol>()).Returns(@"
            /// <summary>
            /// Determines whether or not the specified argument exist.
            /// </summary>
            /// <param name=""context"">The context.</param>
            /// <param name=""name"">The argument name.</param>
            /// <returns>Whether or not the specified argument exist.</returns>
            /// <example>
            /// This sample shows how to call the <see cref=""M:Cake.Common.ArgumentAliases.HasArgument(Cake.Core.ICakeContext,System.String)""/> method.
            /// <code>
            /// var argumentName = ""myArgument"";
            /// //Cake.exe .\hasargument.cake -myArgument=""is specified""
            /// if (HasArgument(argumentName))
            /// {
            ///     Information(""{0} is specified"", argumentName);
            /// }
            /// //Cake.exe .\hasargument.cake
            /// else
            /// {
            ///     Warning(""{0} not specified"", argumentName);
            /// }
            /// </code>
            /// </example>");

            var rewriter = new MethodSyntaxRewriter(semanticModel);

            var result2 = rewriter.Visit(syntaxTree.GetRoot());
            // var result = rewriter.Visit(syntaxTree);
            // var stringResult = result.NormalizeWhitespace().ToFullString();
        }

    }
}