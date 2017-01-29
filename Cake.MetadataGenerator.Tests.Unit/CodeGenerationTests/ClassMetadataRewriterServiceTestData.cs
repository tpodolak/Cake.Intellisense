using System.Collections.Generic;

namespace Cake.MetadataGenerator.Tests.Unit.CodeGenerationTests
{
    public class ClassMetadataRewriterServiceTestData
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                yield return new object[] { RemovesFieldsDeclaration() };
                yield return new object[] { RemovesPropertyDeclarations() };
                yield return new object[] { RemovesNonPublicClasses() };
                yield return new object[] { AppendsMetadataClassSufixToClassName() };
                yield return new object[] { RemovesAllConstructor() };
                yield return new object[] { ReplacesClassModifierWithPublicOne() };
                yield return new object[] { RemovesNonPublicMethods() };
                yield return new object[] { RemovesBaseList() };

            }
        }

        private static ServiceRewriterTestCase RemovesFieldsDeclaration()
        {
            return new ServiceRewriterTestCase(
                nameof(RemovesFieldsDeclaration),
                @"namespace Cake.Common
{
    public static class ArgumentAliases
    {
        private static string privateField = ""MyValue"";
        protected static string protectedField = ""MyValue"";
        public static string publicField = ""MyValue"";

        [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
        public static T Argument<T>(this global::Cake.Core.ICakeContext context, System.String name)
        {
        }
    }
}",
                @"namespace Cake.Common
{
    public static class ArgumentAliasesMetadata
    {
        [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
        public static T Argument<T>(this global::Cake.Core.ICakeContext context, System.String name)
        {
        }
    }
}"
            );
        }

        private static ServiceRewriterTestCase RemovesPropertyDeclarations()
        {
            return new ServiceRewriterTestCase(
                nameof(RemovesPropertyDeclarations),
                @"namespace Cake.Common
{
    public static class ArgumentAliases
    {
        private static string PrivateProperty { get; }
        protected static string ProtectedProperty { get; }
        public static string PublicProperty { get; }

        [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
        public static T Argument<T>(this global::Cake.Core.ICakeContext context, System.String name)
        {
        }
    }
}",
                @"namespace Cake.Common
{
    public static class ArgumentAliasesMetadata
    {
        [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
        public static T Argument<T>(this global::Cake.Core.ICakeContext context, System.String name)
        {
        }
    }
}"
            );
        }

        private static ServiceRewriterTestCase RemovesNonPublicClasses()
        {
            return new ServiceRewriterTestCase(
                nameof(RemovesNonPublicClasses),
                @"namespace Cake.Common
{
    internal static class InternalArgumentAliases
    {
    }

    public static class ArgumentAliases
    {
    }
}",
                @"namespace Cake.Common
{
    public static class ArgumentAliasesMetadata
    {
    }
}"
            );
        }

        private static ServiceRewriterTestCase AppendsMetadataClassSufixToClassName()
        {
            return new ServiceRewriterTestCase(
                nameof(AppendsMetadataClassSufixToClassName),
                @"namespace Cake.Common
{
    public static class ArgumentAliases
    {
    }
}",
                @"namespace Cake.Common
{
    public static class ArgumentAliasesMetadata
    {
    }
}"
            );
        }

        private static ServiceRewriterTestCase RemovesAllConstructor()
        {
            return new ServiceRewriterTestCase(
                nameof(RemovesAllConstructor),
                @"namespace Cake.Core.Scripting
{
    public class ScriptHost
    {
        public ScriptHost()
        {
        }

        protected ScriptHost(global::Cake.Core.ICakeEngine engine, global::Cake.Core.ICakeContext context)
        {
        }
    }
}",
                @"namespace Cake.Core.Scripting
{
    public class ScriptHostMetadata
    {
    }
}"
            );
        }

        private static ServiceRewriterTestCase ReplacesClassModifierWithPublicOne()
        {
            return new ServiceRewriterTestCase(
                nameof(ReplacesClassModifierWithPublicOne),
                @"namespace Cake.Core.Scripting
{
    public abstract class ScriptHost
    {
    }
}",
                @"namespace Cake.Core.Scripting
{
    public class ScriptHostMetadata
    {
    }
}"
            );
        }

        private static ServiceRewriterTestCase RemovesNonPublicMethods()
        {
            return new ServiceRewriterTestCase(
                nameof(RemovesNonPublicMethods),
                @"namespace Cake.Common
{
    public static class ArgumentAliases
    {
        public static T Argument<T>(this global::Cake.Core.ICakeContext context, System.String name)
        {
        }
        
        private static T Argument<T>(System.String name)
        {
        }

        protected static T Argument<T>(System.String name)
        {
        }
    }
}",
                @"namespace Cake.Common
{
    public static class ArgumentAliasesMetadata
    {
        public static T Argument<T>(this global::Cake.Core.ICakeContext context, System.String name)
        {
        }
    }
}"
            );
        }

        private static ServiceRewriterTestCase RemovesBaseList()
        {
            return new ServiceRewriterTestCase(
                nameof(RemovesBaseList),
                @"namespace Cake.Common
{
    public static class ArgumentAliases: System.Object
    {
    }
}",
                @"namespace Cake.Common
{
    public static class ArgumentAliasesMetadata
    {
    }
}"
            );

        }
    }
}
