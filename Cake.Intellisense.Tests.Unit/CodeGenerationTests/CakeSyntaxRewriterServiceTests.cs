using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.CakeSyntaxRewriters;
using Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.Interfaces;
using Cake.Intellisense.Compilation.Interfaces;
using Cake.Intellisense.Tests.Unit.Common;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using MoreLinq;
using NSubstitute;
using Xunit;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Cake.Intellisense.Tests.Unit.CodeGenerationTests
{
    public partial class CakeSyntaxRewriterServiceTests
    {
        public class RewriteMethod : Test<CakeSyntaxRewriterService>
        {
            public RewriteMethod()
            {
                var compilation = Use<Microsoft.CodeAnalysis.Compilation>(string.Empty, null, null, false, null);
                compilation.SyntaxTrees.Returns(new List<SyntaxTree> { CSharpSyntaxTree.ParseText(string.Empty) });
                compilation.AddSyntaxTrees(Arg.Any<SyntaxTree[]>()).Returns(compilation);
                compilation.ReplaceSyntaxTree(Arg.Any<SyntaxTree>(), Arg.Any<SyntaxTree>()).Returns(compilation);
                Get<ICompilationProvider>().Get(Arg.Any<string>()).Returns(compilation);
            }

            [Fact]
            public void CallsRewritersInProperOrder()
            {
                var syntaxRewriterServices = Get<IEnumerable<ISyntaxRewriterService>>().ToList();
                syntaxRewriterServices.ForEach(
                    val =>
                        val.Rewrite(Arg.Any<Assembly>(), Arg.Any<SemanticModel>(), Arg.Any<SyntaxNode>())
                            .Returns(CSharpSyntaxTree.ParseText(string.Empty).GetRoot()));

                Subject.Rewrite(CompilationUnit(), GetType().Assembly);

                Received.InOrder(() =>
                {
                    foreach (var syntaxRewriterService in syntaxRewriterServices.OrderBy(val => val.Order))
                        syntaxRewriterService.Rewrite(Arg.Any<Assembly>(), Arg.Any<SemanticModel>(), Arg.Any<SyntaxNode>());
                });
            }

            [Fact]
            public void RepacesSyntaxTreeAfterEveryRewrite()
            {
                var syntaxRewriterServices = Get<IEnumerable<ISyntaxRewriterService>>().OrderBy(val => val.Order).ToList();
                syntaxRewriterServices.ForEach(service => service.Rewrite(Arg.Any<Assembly>(), Arg.Any<SemanticModel>(), Arg.Any<SyntaxNode>()).Returns(CSharpSyntaxTree.ParseText(string.Empty).GetRoot()));

                Subject.Rewrite(CompilationUnit(), GetType().Assembly);

                Received.InOrder(() =>
                {
                    foreach (var syntaxRewriterService in syntaxRewriterServices)
                    {
                        syntaxRewriterService.Rewrite(Arg.Any<Assembly>(), Arg.Any<SemanticModel>(), Arg.Any<SyntaxNode>());
                        Get<Microsoft.CodeAnalysis.Compilation>().ReplaceSyntaxTree(Arg.Any<SyntaxTree>(), Arg.Any<SyntaxTree>());
                    }
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