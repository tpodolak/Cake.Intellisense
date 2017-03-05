using Cake.MetadataGenerator.CodeGeneration.SyntaxRewriterServices.MethodRewriters;

namespace Cake.MetadataGenerator.Tests.Unit.CodeGenerationTests
{
    public partial class MethodSyntaxRewriterServiceTest
    {
        public class RewriteMethod: SyntaxRewriterServiceTest<MethodSyntaxRewriterService>
        {
            static RewriteMethod()
        {
            TestCases = new[]
            {
                new object[] { RemovesCakeContextParamWhenMethodIsCakeAlias() },
                new object[] { KeepsParametersIntactsWhenMethodNotDecoratedWithCakeMethodAliasAttribute() },
                new object[] { KeepsMethodIntactWhenMethodDecoratedWithCakeMethodAliasAttributeButHasNoParameters() },
                new object[] { ConvertsMethodDecoratedWithCakePropertyAliasAttributeToProperty() },
                new object[] { AppendMethodBodyWithProperReturnsStatementsWhemMethodReturnsValue() },
                new object[] { AppendMethodBodyWithOutParametersAssignedWhenMethodHasOutParameters() },
                new object[] { ConvertsAbstractMethodToPublicStaticMethods() },
            };
        }

        private static ServiceRewriterTestCase RemovesCakeContextParamWhenMethodIsCakeAlias()
        {
            return new ServiceRewriterTestCase(
                nameof(RemovesCakeContextParamWhenMethodIsCakeAlias),
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
}"
            );
        }

        private static ServiceRewriterTestCase KeepsParametersIntactsWhenMethodNotDecoratedWithCakeMethodAliasAttribute()
        {
            return new ServiceRewriterTestCase(
                nameof(KeepsParametersIntactsWhenMethodNotDecoratedWithCakeMethodAliasAttribute),
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
}"
            );
        }

        private static ServiceRewriterTestCase KeepsMethodIntactWhenMethodDecoratedWithCakeMethodAliasAttributeButHasNoParameters()
        {
            return new ServiceRewriterTestCase(
                nameof(KeepsMethodIntactWhenMethodDecoratedWithCakeMethodAliasAttributeButHasNoParameters),
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
}"
            );
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
}"
            );
        }

        private static ServiceRewriterTestCase AppendMethodBodyWithProperReturnsStatementsWhemMethodReturnsValue()
        {
            return new ServiceRewriterTestCase(
                nameof(AppendMethodBodyWithProperReturnsStatementsWhemMethodReturnsValue),
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
}"
            );
        }

        private static ServiceRewriterTestCase AppendMethodBodyWithOutParametersAssignedWhenMethodHasOutParameters()
        {
            return new ServiceRewriterTestCase(
                nameof(AppendMethodBodyWithOutParametersAssignedWhenMethodHasOutParameters),
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
}"
            );
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
    
}"
            );
        }
    }
    }
}