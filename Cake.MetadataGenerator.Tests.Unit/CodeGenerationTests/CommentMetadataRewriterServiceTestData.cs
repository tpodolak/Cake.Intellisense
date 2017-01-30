using System.Collections.Generic;

namespace Cake.MetadataGenerator.Tests.Unit.CodeGenerationTests
{
    public class CommentMetadataRewriterServiceTestData
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                yield return new object[] { ProperlyAppendsXmlCommentToMethodWithoutAttributes() };
            }
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
@"public abstract class ScriptHost
{
///<summary>
///Registers a new task.
///</summary>
///<param name=""name"">The name of the task.</param>
///<returns>A <see cref=""T:Cake.Core.CakeTaskBuilder`1"" />.</returns>
public void Task(System.String name)
    {
    }
}"
            );
        }
    }
}