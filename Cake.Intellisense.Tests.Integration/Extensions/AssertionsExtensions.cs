using System.Reflection;
using Cake.Intellisense.CodeGeneration.MetadataGenerators;
using Cake.Intellisense.Tests.Integration.Assertions;

namespace Cake.Intellisense.Tests.Integration.Extensions
{
    public static class AssertionsExtensions
    {
        public static GeneratorResultAssertions Should(this GeneratorResult subject)
        {
            return new GeneratorResultAssertions(subject);
        }
    }
}