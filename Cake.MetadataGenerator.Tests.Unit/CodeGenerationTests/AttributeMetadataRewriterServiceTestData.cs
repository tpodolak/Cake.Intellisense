﻿using System.Collections.Generic;

namespace Cake.MetadataGenerator.Tests.Unit.CodeGenerationTests
{
    public class AttributeMetadataRewriterServiceTestData
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                yield return new object[] { RemovesCakeMethodAliasAttribute() };
                yield return new object[] { RemovesCakeAliasCategoryAttribute() };
                yield return new object[] { RemovesCakeNamespaceImportAttribue() };
                yield return new object[] { RemovesCakePropertyAttributesFromMethodsTest() };
                yield return new object[] { RemovesCakePropertyAttributesFromPropertiesTest() };
                yield return new object[] { KeepsCommentTriviaWhenRemovingCakeAttributes() };
                yield return new object[] { KeepsAttributesNotRelatedWithCakeIntact() };
                yield return new object[] { RemovesParametersFromObosoleteAttribute() };
            }
        }

        private static ServiceRewriterTestCase RemovesCakeMethodAliasAttribute()
        {
            return new ServiceRewriterTestCase(
                nameof(RemovesCakeMethodAliasAttribute),
@"public static class ArgumentAliases
{
    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static T Argument<T>(this global::Cake.Core.ICakeContext context, System.String name)
    {
    }

    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static T Argument<T>(this global::Cake.Core.ICakeContext context, System.String name, T defaultValue)
    {
    }
}",
@"public static class ArgumentAliases
{
    public static T Argument<T>(this global::Cake.Core.ICakeContext context, System.String name)
    {
    }

    public static T Argument<T>(this global::Cake.Core.ICakeContext context, System.String name, T defaultValue)
    {
    }
}"
            );
        }

        private static ServiceRewriterTestCase RemovesCakeAliasCategoryAttribute()
        {
            return new ServiceRewriterTestCase(
                nameof(RemovesCakeAliasCategoryAttribute),
@"[global::Cake.Core.Annotations.CakeAliasCategoryAttribute(null)]
public static class ArgumentAliases
{
}

[global::Cake.Core.Annotations.CakeAliasCategoryAttribute(null)]
public static class EnvironmentAliases
{
}",
@"public static class ArgumentAliases
{
}

public static class EnvironmentAliases
{
}"
            );
        }

        private static ServiceRewriterTestCase RemovesCakeNamespaceImportAttribue()
        {
            return new ServiceRewriterTestCase(
                nameof(RemovesCakeNamespaceImportAttribue),
@"public static class BuildSystemAliases
{
    [global::Cake.Core.Annotations.CakeNamespaceImportAttribute(null), global::Cake.Core.Annotations.CakeNamespaceImportAttribute(null)]
    public static global::Cake.Common.Build.AppVeyor.IAppVeyorProvider AppVeyor(this global::Cake.Core.ICakeContext context)
    {
    }

    [global::Cake.Core.Annotations.CakeNamespaceImportAttribute(null)]
    public static global::Cake.Common.Build.Bamboo.IBambooProvider Bamboo(this global::Cake.Core.ICakeContext context)
    {
    }
}",
@"public static class BuildSystemAliases
{
    public static global::Cake.Common.Build.AppVeyor.IAppVeyorProvider AppVeyor(this global::Cake.Core.ICakeContext context)
    {
    }

    public static global::Cake.Common.Build.Bamboo.IBambooProvider Bamboo(this global::Cake.Core.ICakeContext context)
    {
    }
}"
            );
        }

        private static ServiceRewriterTestCase RemovesCakePropertyAttributesFromMethodsTest()
        {
            return new ServiceRewriterTestCase(
                nameof(RemovesCakePropertyAttributesFromMethodsTest),
@"public static class BuildSystemAliases
{
    [global::Cake.Core.Annotations.CakePropertyAliasAttribute(Cache = true)]
    public static global::Cake.Common.Build.Bamboo.IBambooProvider Bamboo(this global::Cake.Core.ICakeContext context)
    {
    }
}",
@"public static class BuildSystemAliases
{
    public static global::Cake.Common.Build.Bamboo.IBambooProvider Bamboo(this global::Cake.Core.ICakeContext context)
    {
    }
}"
            );
        }

        private static ServiceRewriterTestCase RemovesCakePropertyAttributesFromPropertiesTest()
        {
            return new ServiceRewriterTestCase(
                nameof(RemovesCakePropertyAttributesFromPropertiesTest),
@"public static class BuildSystemAliases
{
    [global::Cake.Core.Annotations.CakePropertyAliasAttribute(Cache = true)]
    public static global::Cake.Common.Build.Bamboo.IBambooProvider Bamboo
    {
        get;
    }
}",
@"public static class BuildSystemAliases
{
    public static global::Cake.Common.Build.Bamboo.IBambooProvider Bamboo
    {
        get;
    }
}"
            );
        }

        private static ServiceRewriterTestCase KeepsCommentTriviaWhenRemovingCakeAttributes()
        {
            return new ServiceRewriterTestCase(
                nameof(KeepsCommentTriviaWhenRemovingCakeAttributes),
@"/// <summary>
///  Contains functionality related to build systems.
/// </summary>
[global::Cake.Core.Annotations.CakeAliasCategoryAttribute(null)]
public static class BuildSystemAliases
{
    /// <summary>
    /// Gets a <see cref=""T:Cake.Common.Build.BuildSystem""/> instance that can
    /// be used to query for information about the current build system.
    /// </summary>
    [global::Cake.Core.Annotations.CakeNamespaceImportAttribute(null)]
    [global::Cake.Core.Annotations.CakePropertyAliasAttribute(Cache = true)]
    public static global::Cake.Common.Build.BuildSystem BuildSystem(this global::Cake.Core.ICakeContext context)
    {
    }

    /// <summary>
    /// Gets a <see cref=""T: Cake.Common.Build.TravisCI.TravisCIProvider""/> instance that can be used to
    /// obtain information from the Travis CI environment.
    /// </summary>
    [global::Cake.Core.Annotations.CakeMethodAliasAttribute]
    public static global::Cake.Common.Build.TravisCI.ITravisCIProvider TravisCI(this global::Cake.Core.ICakeContext context)
    {
    }
}",
@"/// <summary>
///  Contains functionality related to build systems.
/// </summary>
public static class BuildSystemAliases
{
    /// <summary>
    /// Gets a <see cref=""T:Cake.Common.Build.BuildSystem""/> instance that can
    /// be used to query for information about the current build system.
    /// </summary>
    public static global::Cake.Common.Build.BuildSystem BuildSystem(this global::Cake.Core.ICakeContext context)
    {
    }

    /// <summary>
    /// Gets a <see cref=""T: Cake.Common.Build.TravisCI.TravisCIProvider""/> instance that can be used to
    /// obtain information from the Travis CI environment.
    /// </summary>
    public static global::Cake.Common.Build.TravisCI.ITravisCIProvider TravisCI(this global::Cake.Core.ICakeContext context)
    {
    }
}"
            );
        }

        private static ServiceRewriterTestCase KeepsAttributesNotRelatedWithCakeIntact()
        {
            return new ServiceRewriterTestCase(
                nameof(KeepsAttributesNotRelatedWithCakeIntact),
@"[Obsolete]
public static class BuildSystemAliases
{
    [global::Cake.Core.Annotations.CakePropertyAliasAttribute(Cache = true),Obsolete]
    public static global::Cake.Common.Build.Bamboo.IBambooProvider Bamboo(this global::Cake.Core.ICakeContext context)
    {
    }
}",
@"[Obsolete]
public static class BuildSystemAliases
{
    [Obsolete]
    public static global::Cake.Common.Build.Bamboo.IBambooProvider Bamboo(this global::Cake.Core.ICakeContext context)
    {
    }
}"
            );
        }

        private static ServiceRewriterTestCase RemovesParametersFromObosoleteAttribute()
        {
            return new ServiceRewriterTestCase(
                nameof(RemovesParametersFromObosoleteAttribute),
@"[Obsolete(""null"",true)]
public static class BuildSystemAliases
{
    [global::Cake.Core.Annotations.CakePropertyAliasAttribute(Cache = true),Obsolete(""null"",true)]
    public static global::Cake.Common.Build.Bamboo.IBambooProvider Bamboo(this global::Cake.Core.ICakeContext context)
    {
    }
}",
@"[Obsolete]
public static class BuildSystemAliases
{
    [Obsolete]
    public static global::Cake.Common.Build.Bamboo.IBambooProvider Bamboo(this global::Cake.Core.ICakeContext context)
    {
    }
}"
            );
        }
    }
}