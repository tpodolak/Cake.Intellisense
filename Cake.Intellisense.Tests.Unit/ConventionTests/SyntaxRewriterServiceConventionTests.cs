using System;
using System.Linq;
using Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Cake.MetadataGenerator.Tests.Unit.ConventionTests
{
    public class SyntaxRewriterServiceConventionTests
    {
        [Fact]
        public void AllRewriterServicesShouldHaveUniqueOrderPropertyValue()
        {
            var syntaxRewriterType = typeof(ISyntaxRewriterService);
            var syntaxRewriters = syntaxRewriterType.Assembly
                                                    .GetExportedTypes()
                                                    .Where(type => type.GetInterfaces().Any(@interface => @interface == syntaxRewriterType))
                                                    .Select(type => (ISyntaxRewriterService)Substitute.For(new[] { type }, MockConstructorArguments(type)))
                                                    .ToList();

            syntaxRewriters.Count.Should().BeGreaterThan(0);
            syntaxRewriters.Select(val => val.Order).Should().OnlyHaveUniqueItems();
        }

        private object[] MockConstructorArguments(Type type)
        {
            return type.GetConstructors().First().GetParameters().Select(val => Substitute.For(new[] { val.ParameterType }, null)).ToArray();
        }
    }
}