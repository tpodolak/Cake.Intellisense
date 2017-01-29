namespace Cake.MetadataGenerator.Tests.Unit.CodeGenerationTests
{
    public class ServiceRewriterTestCase
    {
        public ServiceRewriterTestCase(string name, string input, string expectedResult)
        {
            this.Name = name;
            this.Input = input;
            this.ExpectedResult = expectedResult;
        }

        public string Name { get; set; }

        public string Input { get; set; }

        public string ExpectedResult { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
