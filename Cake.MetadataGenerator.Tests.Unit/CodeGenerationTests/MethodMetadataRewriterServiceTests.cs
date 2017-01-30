using Cake.MetadataGenerator.CodeGeneration.MetadataRewriterServices.MethodRewriters;

namespace Cake.MetadataGenerator.Tests.Unit.CodeGenerationTests
{
    public class MethodMetadataRewriterServiceTests : MetadataRewriterServiceTests<MethodMetadataRewriterService>
    {
        static MethodMetadataRewriterServiceTests()
        {
            TestCases = MethodMetadataRewriterServiceTestData.TestData;
        }
    }
}