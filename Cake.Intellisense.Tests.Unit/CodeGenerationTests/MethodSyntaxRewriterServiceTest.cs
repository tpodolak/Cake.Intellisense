using System.Collections.Generic;
using Cake.Intellisense.CodeGeneration.SyntaxRewriterServices.MethodRewriters;
using Cake.Intellisense.Tests.Unit.Common;

namespace Cake.Intellisense.Tests.Unit.CodeGenerationTests
{
    public partial class MethodSyntaxRewriterServiceTest
    {
        public class RewriteMethod : SyntaxRewriterServiceTest<MethodSyntaxRewriterService>
        {
            static RewriteMethod()
            {
                TestCases = new List<ServiceRewriterTestCase>
                {
                    RemovesCakeContextParam_WhenMethodIsCakeAlias(),
                    KeepsParametersIntacts_WhenMethodNotDecoratedWithCakeMethodAliasAttribute(),
                    KeepsMethodIntact_WhenMethodDecoratedWithCakeMethodAliasAttributeButHasNoParameters(),
                    ConvertsMethodDecoratedWithCakePropertyAliasAttributeToProperty(),
                    AppendMethodBodyWithProperReturnsStatements_WhenMethodReturnsValue(),
                    AppendMethodBodyWithOutParametersAssigned_WhenMethodHasOutParameters(),
                    ConvertsAbstractMethodToPublicStaticMethods()
                };
            }

            private static ServiceRewriterTestCase RemovesCakeContextParam_WhenMethodIsCakeAlias()
            {
                return new ServiceRewriterTestCase(
                    nameof(RemovesCakeContextParam_WhenMethodIsCakeAlias),
                    @"public static class ArgumentAliases
{
    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static void Argument<T>(this global::Cake.Core.ICakeContext context, System.String name)
    {
    }
}",
                    @"public static class ArgumentAliases
{
    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static void Argument<T>(System.String name)
    {
    }
}");
            }

            private static ServiceRewriterTestCase KeepsParametersIntacts_WhenMethodNotDecoratedWithCakeMethodAliasAttribute()
            {
                return new ServiceRewriterTestCase(
                    nameof(KeepsParametersIntacts_WhenMethodNotDecoratedWithCakeMethodAliasAttribute),
                    @"public static class ArgumentAliases
{
    public static void Argument<T>(this global::Cake.Core.ICakeContext context, System.String name)
    {
    }
}",
                    @"public static class ArgumentAliases
{
    public static void Argument<T>(this global::Cake.Core.ICakeContext context, System.String name)
    {
    }
}");
            }

            private static ServiceRewriterTestCase KeepsMethodIntact_WhenMethodDecoratedWithCakeMethodAliasAttributeButHasNoParameters()
            {
                return new ServiceRewriterTestCase(
                    nameof(KeepsMethodIntact_WhenMethodDecoratedWithCakeMethodAliasAttributeButHasNoParameters),
                    @"public static class ArgumentAliases
{
    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static void Argument<T>()
    {
    }
}",
                    @"public static class ArgumentAliases
{
    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static void Argument<T>()
    {
    }
}");
            }

            private static ServiceRewriterTestCase ConvertsMethodDecoratedWithCakePropertyAliasAttributeToProperty()
            {
                return new ServiceRewriterTestCase(
                    nameof(ConvertsMethodDecoratedWithCakePropertyAliasAttributeToProperty),
                    @"public static class BuildSystemAliases
{
    [global::Cake.Core.Annotations.CakePropertyAliasAttribute(Cache = true)]
    public static global::Cake.Common.Build.AppVeyor.IAppVeyorProvider AppVeyor(this global::Cake.Core.ICakeContext context)
    {
    }
}",
                    @"public static class BuildSystemAliases
{
    [global::Cake.Core.Annotations.CakePropertyAliasAttribute(Cache = true)]
    public static global::Cake.Common.Build.AppVeyor.IAppVeyorProvider AppVeyor
    {
        get;
    }
}");
            }

            private static ServiceRewriterTestCase AppendMethodBodyWithProperReturnsStatements_WhenMethodReturnsValue()
            {
                return new ServiceRewriterTestCase(
                    nameof(AppendMethodBodyWithProperReturnsStatements_WhenMethodReturnsValue),
                    @"public static class ArgumentAliases
{
    public static T Argument<T>(this global::Cake.Core.ICakeContext context, System.String name)
    {
    }

     public static System.Boolean HasArgument(this global::Cake.Core.ICakeContext context, System.String name)
     {
     }
}",
                    @"public static class ArgumentAliases
{
    public static T Argument<T>(this global::Cake.Core.ICakeContext context, System.String name)
    {
        return default (T);
    }

    public static System.Boolean HasArgument(this global::Cake.Core.ICakeContext context, System.String name)
    {
        return default (System.Boolean);
    }
}");
            }

            private static ServiceRewriterTestCase AppendMethodBodyWithOutParametersAssigned_WhenMethodHasOutParameters()
            {
                return new ServiceRewriterTestCase(
                    nameof(AppendMethodBodyWithOutParametersAssigned_WhenMethodHasOutParameters),
                    @"public static class ProcessAliases
{
    public static void StartProcess(this global::Cake.Core.ICakeContext context, ref global::Cake.Core.IO.FilePath fileName, out global::Cake.Core.IO.ProcessSettings settings, out global::System.Collections.Generic.IEnumerable<System.String> redirectedOutput)
    {
    }
}",
                    @"public static class ProcessAliases
{
    public static void StartProcess(this global::Cake.Core.ICakeContext context, ref global::Cake.Core.IO.FilePath fileName, out global::Cake.Core.IO.ProcessSettings settings, out global::System.Collections.Generic.IEnumerable<System.String> redirectedOutput)
    {
        settings = default (global::Cake.Core.IO.ProcessSettings);
        redirectedOutput = default (global::System.Collections.Generic.IEnumerable<System.String>);
    }
}");
            }

            private static ServiceRewriterTestCase ConvertsAbstractMethodToPublicStaticMethods()
            {
                return new ServiceRewriterTestCase(
                    nameof(ConvertsAbstractMethodToPublicStaticMethods),
                    @"public abstract class ScriptHost
{
    public abstract global::Cake.Core.CakeReport RunTarget(System.String target);
}",
                    @"public abstract class ScriptHost
{
    public static global::Cake.Core.CakeReport RunTarget(System.String target)
    {
        return default (global::Cake.Core.CakeReport);
    }
    
}");
            }
        }
    }
}