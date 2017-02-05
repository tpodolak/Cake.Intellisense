using Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices.ClassRewriters;

namespace Cake.MetadataGenerator.Tests.Unit.CodeGenerationTests
{
    public class ClassMetadataRewriterServiceTests : MetadataRewriterServiceTests<ClassSyntaxRewriterService>
    {
        static ClassMetadataRewriterServiceTests()
        {
            TestCases = ClassMetadataRewriterServiceTestData.TestData;
        }
    }
}