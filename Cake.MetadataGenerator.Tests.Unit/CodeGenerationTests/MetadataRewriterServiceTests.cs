﻿using System.Collections.Generic;
using Cake.MetadataGenerator.CodeGeneration.MetadataRewriterServices;
using Cake.MetadataGenerator.Tests.Unit.ExtensionPoints;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Cake.MetadataGenerator.Tests.Unit.CodeGenerationTests
{
    public abstract class MetadataRewriterServiceTests<T> : TestOf<T> where T : class, IMetadataRewriterService
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