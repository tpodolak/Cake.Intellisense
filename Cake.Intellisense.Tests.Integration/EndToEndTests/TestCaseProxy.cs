using System;
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
            var result = _application.Run(args);
            Assert(() => result.Should().NotBeNull().And.GenerateValidCakeAssemblies());
        }

        public void VerifyCakeCorePackage(string[] args)
        {
            var result = _application.Run(args);
            Assert(() => result.Should().NotBeNull().And.GenerateValidCakeCoreAssemblies());
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