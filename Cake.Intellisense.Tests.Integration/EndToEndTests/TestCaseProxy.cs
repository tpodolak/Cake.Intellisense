using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cake.Intellisense.CodeGeneration.MetadataGenerators;
using Cake.Intellisense.Tests.Integration.Exceptions;
using Cake.Intellisense.Tests.Integration.Extensions;
using Xunit.Sdk;

namespace Cake.Intellisense.Tests.Integration.EndToEndTests
{
    public class TestCaseProxy : MarshalByRefObject
    {
        private readonly Application _application = new Application();

        public void VerifyCakePackage(string[] args)
        {
            var result = Run(args);
            Assert(() => result.Should().NotBeNull().And.GenerateValidCakeAssemblies());
        }

        public void VerifyCakeCorePackage(string[] args)
        {
            var result = Run(args);
            Assert(() => result.Should().NotBeNull().And.GenerateValidCakeCoreAssemblies());
        }

        private GeneratorResult Run(string[] args)
        {
            var result = _application.Run(args);
            CopyReferencedAssemblies(result);
            return result;
        }

        private void CopyReferencedAssemblies(GeneratorResult result)
        {
            var referencedAssemblies = result?.EmitedAssemblies
                                           .SelectMany(assembly => assembly.GetReferencedAssemblies()
                                               .Select(val => val.FullName))
                                           .ToList() ?? new List<string>();

            var locations = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(assembly => referencedAssemblies.Contains(assembly.FullName))
                .Select(assembly => assembly.Location);

            foreach (var val in locations.Where(location => !string.IsNullOrWhiteSpace(location)))
            {
                File.Copy(val, Path.Combine(Environment.CurrentDirectory, Path.GetFileName(val)), true);
            }
        }

        private void Assert(Action assertAction)
        {
            try
            {
                assertAction();
            }
            catch (XunitException e)
            {
                throw new SerializableAssertionException(e.Message);
            }
        }
    }
}