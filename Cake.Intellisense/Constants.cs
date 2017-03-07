namespace Cake.Intellisense
{
    public static class RoslynCodeGeneration
    {
        public const string CSharpCodeGenerationServiceTypeName = "Microsoft.CodeAnalysis.CSharp.CodeGeneration.CSharpCodeGenerationService";
        public const string CreateNamedTypeDeclarationMethodName = "CreateNamedTypeDeclaration";
    }

    public static class CakeAttributeNames
    {
        public const string CakeMethodAlias = "CakeMethodAliasAttribute";
        public const string CakeAliasCategory = "CakeAliasCategoryAttribute";
        public const string CakePropertyAlias = "CakePropertyAliasAttribute";
        public const string CakeNamespaceImport = "CakeNamespaceImportAttribute";
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