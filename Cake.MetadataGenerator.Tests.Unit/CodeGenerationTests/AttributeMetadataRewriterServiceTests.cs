using Cake.MetadataGenerator.CodeGeneration.MetadataRewriterServices.AttributeRewriters;

namespace Cake.MetadataGenerator.Tests.Unit.CodeGenerationTests
{
    public class AttributeMetadataRewriterServiceTests : MetadataRewriterServiceTests<AttributeMetadataRewriterService>
    {
        static AttributeMetadataRewriterServiceTests()
        {
            TestCases = AttributeMetadataRewriterServiceTestData.TestData;
        }
    }
}