using System.Xml.Linq;
using Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices.CommentRewriters;
using Cake.MetadataGenerator.Documentation;
using Cake.MetadataGenerator.Tests.Unit.Common;
using Microsoft.CodeAnalysis;
using NSubstitute;

namespace Cake.MetadataGenerator.Tests.Unit.CodeGenerationTests
{
    public partial class CommentSyntaxRewriterServiceTest
    {
        public class RewriteMethod : SyntaxRewriterServiceTest<CommentSyntaxRewriterService>
        {
            private static string xmlComment = @"///<summary>
///Registers a new task.
///</summary>
///<param name=""name"">The name of the task.</param>
///<returns>A <see cref=""T:Cake.Core.CakeTaskBuilder`1"" />.</returns>";

            static RewriteMethod()
            {
                TestCases = new[]
                {
                new object[] { ProperlyAppendsXmlCommentToMethodWithoutAttributes() },
                new object[] { ProperlyAppendsXmlCommentToMethodWithAttributes() }
            };
            }

            public RewriteMethod()
            {
                Get<ICommentProvider>().Get(Arg.Any<XDocument>(), Arg.Any<ISymbol>())
                                          .Returns(xmlComment);
            }

            private static ServiceRewriterTestCase ProperlyAppendsXmlCommentToMethodWithoutAttributes()
            {
                return new ServiceRewriterTestCase(
                    nameof(ProperlyAppendsXmlCommentToMethodWithoutAttributes),
    @"public abstract class ScriptHost
{
    public void Task(System.String name)
    {
    }
}",
    $@"public abstract class ScriptHost
{{
{xmlComment}
public void Task(System.String name)
    {{
    }}
}}");
            }

            private static ServiceRewriterTestCase ProperlyAppendsXmlCommentToMethodWithAttributes()
            {
                return new ServiceRewriterTestCase(
                    nameof(ProperlyAppendsXmlCommentToMethodWithAttributes),
    @"public abstract class ScriptHost
{
    [CakeMethodAliasAttribute]
    public void Task(System.String name)
    {
    }
}",
    $@"public abstract class ScriptHost
{{
{xmlComment}
[CakeMethodAliasAttribute]
    public void Task(System.String name)
    {{
    }}
}}");
            }
        }
    }
}