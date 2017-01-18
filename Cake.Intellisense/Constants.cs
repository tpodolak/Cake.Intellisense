namespace Cake.MetadataGenerator
{
    public static class RoslynCodeGeneration
    {
        public const string CSharpCodeGenerationServiceTypeName = "Microsoft.CodeAnalysis.CSharp.CodeGeneration.CSharpCodeGenerationService",
            CreateNamedTypeDeclarationMethodName = "CreateNamedTypeDeclaration";
    }

    public static class CakeAttributes
    {
        public const string CakeMethodAlias = "CakeMethodAliasAttribute",
            CakeAliasCategory = "CakeAliasCategoryAttribute",
            CakePropertyAlias = "CakePropertyAliasAttribute",
            CakeNamespaceImport = "CakeNamespaceImportAttribute";
    }

    public static class PackageSources
    {
        public const string NuGetPackageSource = "https://packages.nuget.org/api/v2";
    }

    public static class MetadataGeneration
    {
        public const string MetadataClassSufix = "Metadata";
    }
}