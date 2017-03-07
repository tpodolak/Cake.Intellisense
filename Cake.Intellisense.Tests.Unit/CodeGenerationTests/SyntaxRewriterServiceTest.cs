using System.Collections.Generic;
using Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices;
using Cake.MetadataGenerator.Tests.Unit.Common;
using Cake.MetadataGenerator.Tests.Unit.XUnitExtensionPoints;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Cake.MetadataGenerator.Tests.Unit.CodeGenerationTests
{
    public abstract class SyntaxRewriterServiceTest<T> : Test<T> where T : class, ISyntaxRewriterService
    {
        public static IEnumerable<object[]> TestCases { get; set; }

        [Theory]
        [CustomMemberData(nameof(TestCases))]
        public void Verify(ServiceRewriterTestCase testCase)
        {
            var inputTree = SyntaxFactory.ParseSyntaxTree(testCase.Input);
            var expectedResultTree = SyntaxFactory.ParseSyntaxTree(testCase.ExpectedResult);

            var compilation = CSharpCompilation.Create("name", new[] { inputTree });

            var result = Subject.Rewrite(typeof(T).Assembly, compilation.GetSemanticModel(inputTree), inputTree.GetRoot());

            inputTree.GetDiagnostics().Should().BeEmpty();
            expectedResultTree.GetDiagnostics().Should().BeEmpty();
            result.GetDiagnostics().Should().BeEmpty();
            result.ToFullString().Should().Be(expectedResultTree.GetRoot().ToFullString());
        }
    }
}