namespace Cake.Intellisense
{
    public static class Constants
    {
        public static class RoslynCodeGeneration
        {
            public const string CSharpCodeGenerationServiceTypeName = "Microsoft.CodeAnalysis.CSharp.CodeGeneration.CSharpCodeGenerationService";
            public const string CodeGenerationService = "Microsoft.CodeAnalysis.CodeGeneration.ICodeGenerationService";
            public const string CreateNamedTypeDeclarationMethodName = "CreateNamedTypeDeclaration";
        }

        public static class CakeAttributeNames
        {
            public const string CakeMethodAliasName = "CakeMethodAliasAttribute";
            public const string CakeMethodAliasFullName = "Cake.Core.Annotations.CakeMethodAliasAttribute";
            public const string CakeAliasCategoryName = "CakeAliasCategoryAttribute";
            public const string CakeAliasCategoryFullName = "Cake.Core.Annotations.CakeAliasCategoryAttribute";
            public const string CakePropertyAliasName = "CakePropertyAliasAttribute";
            public const string CakePropertyAliasFullName = "Cake.Core.Annotations.CakePropertyAliasAttribute";
            public const string CakeNamespaceImportName = "CakeNamespaceImportAttribute";
            public const string CakeNamespaceImportFullName = "Cake.Core.Annotations.CakeNamespaceImportAttribute";
        }

        public static class CakeEngineNames
        {
            public const string ScriptHostName = "ScriptHost";
            public const string ScripHostFullName = "Cake.Core.Scripting.ScriptHost";
        }

        public static class MetadataGeneration
        {
            public const string MetadataClassSuffix = "Metadata";
        }
    }
}