using Cake.MetadataGenerator.CommandLine;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Cake.MetadataGenerator.SyntaxRewriters
{
    public class CurrentCompilationContextProvider : ICurrentCompilationContextProvider
    {
        private CompilationContext compilationContext;

        public CompilationContext Get()
        {
            return compilationContext;
        }

        public CompilationContext Get(MetadataGeneratorOptions options)
        {
//            CSharpCompilation.Create(
//                assemblyName: formattableString,
//                syntaxTrees: new SyntaxTree[] { },
//                references: assemblies.Select(y => MetadataReference.CreateFromFile(y.Location)),
//                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            return new CompilationContext(null);
        }
    }

    public interface ICurrentCompilationContextProvider
    {
        CompilationContext Get();

        CompilationContext Get(MetadataGeneratorOptions options);
    }
}