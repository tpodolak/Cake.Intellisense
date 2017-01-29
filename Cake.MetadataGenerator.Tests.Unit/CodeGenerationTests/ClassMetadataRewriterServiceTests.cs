using Cake.MetadataGenerator.CodeGeneration.MetadataRewriterServices.ClassRewriters;

namespace Cake.MetadataGenerator.Tests.Unit.CodeGenerationTests
{
    public class ClassMetadataRewriterServiceTests : MetadataRewriterServiceTests<ClassMetadataRewriterService>
    {
        static ClassMetadataRewriterServiceTests()
        {
            TestCases = ClassMetadataRewriterServiceTestData.TestData;
        }
    }
}