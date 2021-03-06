﻿using System;
using System.Linq;
using Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.Interfaces;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Cake.Intellisense.Tests.Unit.ConventionTests
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