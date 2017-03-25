using System.Collections.Generic;
using Cake.Intellisense.CodeGeneration.SyntaxRewriterServices;
using Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.Interfaces;
using Cake.Intellisense.Tests.Unit.Common;
using Cake.Intellisense.Tests.Unit.XUnitExtensionPoints;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Cake.Intellisense.Tests.Unit.CodeGenerationTests
{
    public abstract class SyntaxRewriterServiceTest<T> : Test<T> where T : class, ISyntaxRewriterService
    {
        public static IEnumerable<object[]> TestCases { get; set; }

        [Theory]
        [CustomMemberData(nameof(TestCases))]
        public void Verify(ServiceRewriterTestCase testCase)
        {
            var inputTree = ParseSyntaxTree(testCase.Input);
            var expectedResultTree = ParseSyntaxTree(testCase.ExpectedResult);

            var compilation = CSharpCompilation.Create("name", new[] { inputTree });

            var result = Subject.Rewrite(typeof(T).Assembly, compilation.GetSemanticModel(inputTree), inputTree.GetRoot());

            inputTree.GetDiagnostics().Should().BeEmpty();
            expectedResultTree.GetDiagnostics().Should().BeEmpty();
            result.GetDiagnostics().Should().BeEmpty();
            result.ToFullString().Should().Be(expectedResultTree.GetRoot().ToFullString());
        }
    }
}