namespace Cake.MetadataGenerator
{
    public static class RoslynCodeGeneration
    {
        public const string CSharpCodeGenerationServiceTypeName = "Microsoft.CodeAnalysis.CSharp.CodeGeneration.CSharpCodeGenerationService",
            CreateNamedTypeDeclarationMethodName = "CreateNamedTypeDeclaration";
    }

    public static class CakeAttributeNames
    {
        public const string CakeMethodAlias = "CakeMethodAliasAttribute",
            CakeAliasCategory = "CakeAliasCategoryAttribute",
            CakePropertyAlias = "CakePropertyAliasAttribute",
            CakeNamespaceImport = "CakeNamespaceImportAttribute";
    }

    public static class CakeEngineNames
    {
        public const string ScriptHost = "ScriptHost";
    }

    public static class MetadataGeneration
    {
        public const string MetadataClassSuffix = "Metadata";
    }
}