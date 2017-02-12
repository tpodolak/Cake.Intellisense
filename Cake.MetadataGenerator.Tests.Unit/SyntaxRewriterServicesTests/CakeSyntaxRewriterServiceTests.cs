using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices;
using Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices.CakeSyntaxRewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using MoreLinq;
using NSubstitute;
using Xunit;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Cake.MetadataGenerator.Tests.Unit.SyntaxRewriterServicesTests
{
    public sealed class CakeSyntaxRewriterServiceTests
    {
        public class RewriteMethod : Test<CakeSyntaxRewriterService>
        {
            [Fact]
            public void ShouldCallRewritersInProperOrder()
            {
                var syntaxRewriterServices = Get<IEnumerable<ISyntaxRewriterService>>().ToList();
                syntaxRewriterServices.ForEach(
                    val =>
                        val.Rewrite(Arg.Any<Assembly>(), Arg.Any<SemanticModel>(), Arg.Any<SyntaxNode>())
                            .Returns(CSharpSyntaxTree.ParseText("").GetRoot()));

                Subject.Rewrite(CompilationUnit(), GetType().Assembly);

                Received.InOrder(() =>
                {
                    foreach (var syntaxRewriterService in syntaxRewriterServices.OrderBy(val => val.Order))
                        syntaxRewriterService.Rewrite(Arg.Any<Assembly>(), Arg.Any<SemanticModel>(), Arg.Any<SyntaxNode>());
                });
            }

            public override object CreateInstance(Type type, params object[] constructorArgs)
            {
                if (type != typeof(IEnumerable<ISyntaxRewriterService>))
                    return base.CreateInstance(type, constructorArgs);

                var syntaxRewriterServices = new[]
                {
                    Substitute.For<ISyntaxRewriterService>(),
                    Substitute.For<ISyntaxRewriterService>(),
                    Substitute.For<ISyntaxRewriterService>()
                };
                syntaxRewriterServices.Reverse().ForEach((item, idx) => item.Order.Returns(idx));
                return syntaxRewriterServices;
            }
        }
    }
}