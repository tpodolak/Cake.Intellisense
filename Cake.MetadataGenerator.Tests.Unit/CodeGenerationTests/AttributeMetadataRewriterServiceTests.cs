using Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices.AttributeRewriters;

namespace Cake.MetadataGenerator.Tests.Unit.CodeGenerationTests
{
    public class AttributeMetadataRewriterServiceTests : MetadataRewriterServiceTests<AttributeSyntaxRewriterService>
    {
        static AttributeMetadataRewriterServiceTests()
        {
            TestCases = AttributeMetadataRewriterServiceTestData.TestData;
        }
    }
}