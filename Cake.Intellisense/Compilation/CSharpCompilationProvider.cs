﻿using System.Collections.Generic;
using Cake.Intellisense.Compilation.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Cake.Intellisense.Compilation
{
    public class CSharpCompilationProvider : ICompilationProvider
    {
        public Microsoft.CodeAnalysis.Compilation Get(string assemblyName, IEnumerable<SyntaxTree> syntaxTrees = null, IEnumerable<MetadataReference> references = null, CSharpCompilationOptions options = null)
        {
            return CSharpCompilation.Create(assemblyName, syntaxTrees, references, options);
        }
    }
}