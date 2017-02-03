using System.Text.RegularExpressions;
using System.Xml.Linq;
using Cake.MetadataGenerator.CodeGeneration.MetadataRewriterServices.CommentRewriters;
using Cake.MetadataGenerator.Documentation;
using Microsoft.CodeAnalysis;
using NSubstitute;

namespace Cake.MetadataGenerator.Tests.Unit.CodeGenerationTests
{
    public class CommentMetadataRewriterServiceTests : MetadataRewriterServiceTests<CommentMetadataRewriterService>
    {
        private static string _xmlComment = @"///<summary>
///Registers a new task.
///</summary>
///<param name=""name"">The name of the task.</param>
///<returns>A <see cref=""T:Cake.Core.CakeTaskBuilder`1"" />.</returns>";

        static CommentMetadataRewriterServiceTests()
        {
            TestCases = new[]
            {
                new object[] {ProperlyAppendsXmlCommentToMethodWithoutAttributes()},
                new object[] { ProperlyAppendsXmlCommentToMethodWithAttributes()}
            };
        }

        public CommentMetadataRewriterServiceTests()
        {
            FakeOf<ICommentProvider>().Get(Arg.Any<XDocument>(), Arg.Any<ISymbol>())
                                      .Returns(_xmlComment);
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
{_xmlComment}
public void Task(System.String name)
    {{
    }}
}}"
            );
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
{_xmlComment}
[CakeMethodAliasAttribute]
    public void Task(System.String name)
    {{
    }}
}}"
            );
        }
    }
}