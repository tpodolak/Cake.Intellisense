﻿using Microsoft.CodeAnalysis;
using NLog;

namespace Cake.MetadataGenerator
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Workspace workspace = new AdhocWorkspace();
            var generator = new MetadataGenerator();
            generator.Generate(args);
        }
    }
}
