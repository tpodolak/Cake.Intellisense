namespace Cake.Intellisense.Tests.Unit.Common
{
    public class ServiceRewriterTestCase
    {
        public ServiceRewriterTestCase(string name, string input, string expectedResult)
        {
            Name = name;
            Input = input;
            ExpectedResult = expectedResult;
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
