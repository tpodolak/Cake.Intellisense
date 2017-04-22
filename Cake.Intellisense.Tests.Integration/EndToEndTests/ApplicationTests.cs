using System;
using System.Collections.Generic;
using System.IO;
using AppDomainToolkit;
using Xunit;

namespace Cake.Intellisense.Tests.Integration.EndToEndTests
{
    public class ApplicationTests
    {
        public class RunMethod
        {
            private const string DefaultFramework = ".NETFramework,Version=v4.5";

            public static IEnumerable<object[]> CakePackages
            {
                get
                {
                    yield return new object[] { CreateMetadataGeneratorOptions("Cake.Common", DefaultFramework, "0.19.1") };
                    yield return new object[] { CreateMetadataGeneratorOptions("Cake.Powershell", DefaultFramework, "0.3.0") };
                    yield return new object[] { CreateMetadataGeneratorOptions("Cake.Coveralls", DefaultFramework, "0.4.0") };
                }
            }

            public static IEnumerable<object[]> CakeCorePackages
            {
                get { yield return new object[] { CreateMetadataGeneratorOptions("Cake.Core", DefaultFramework, "0.19.1") }; }
            }

            [Theory]
            [MemberData(nameof(CakePackages))]
            public void CanGenerateMetadataForPackageTest(string[] options)
            {
                IsolateRun(proxy => proxy.VerifyCakePackage(options));
            }

            [Theory]
            [MemberData(nameof(CakeCorePackages))]
            public void CanGenerateMetadataForCakeCoreLibTest(string[] options)
            {
                IsolateRun(proxy => proxy.VerifyCakeCorePackage(options));
            }

            private void IsolateRun(Action<TestCaseProxy> action)
            {
                var context = AppDomainContext.Create(AppDomain.CurrentDomain.SetupInformation);
                using (var remoteGreeter = Remote<TestCaseProxy>.CreateProxy(context.Domain))
                {
                    action(remoteGreeter.RemoteObject);
                }
            }

            private static string[] CreateMetadataGeneratorOptions(string package, string targetFramework, string version)
            {
                return new[]
                {
                    "--Package", package, "--PackageVersion", version ?? string.Empty, "--TargetFramework",
                    targetFramework,
                    "--OutputFolder", Path.Combine(Environment.CurrentDirectory, "Result")
                };
            }
        }
    }
}