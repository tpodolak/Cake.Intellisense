﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cake.MetadataGenerator.CodeGeneration
{
    public interface ICSharpCodeGenerationService
    {
        ClassDeclarationSyntax CreateNamedTypeDeclaration(INamedTypeSymbol namedTypeSymbol);
    }
}
