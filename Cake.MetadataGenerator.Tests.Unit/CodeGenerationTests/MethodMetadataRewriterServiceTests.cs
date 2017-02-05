using Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices.MethodRewriters;

namespace Cake.MetadataGenerator.Tests.Unit.CodeGenerationTests
{
    public class MethodMetadataRewriterServiceTests : MetadataRewriterServiceTests<MethodSyntaxRewriterService>
    {
        static MethodMetadataRewriterServiceTests()
        {
            TestCases = MethodMetadataRewriterServiceTestData.TestData;
        }
    }
}