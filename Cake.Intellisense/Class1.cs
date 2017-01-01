using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;

namespace Cake.Intellisense
{
    public class Class1
    {
        public void Foo()
        {

            var parameterList = new[]
            {
                SyntaxFactory.Parameter(SyntaxFactory.Identifier("name")).WithType(SyntaxFactory.ParseTypeName("int"))
            };

            var comp = SyntaxFactory.CompilationUnit()
                .AddMembers(
                    SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName("ACO"))
                        .AddMembers(
                            SyntaxFactory.ClassDeclaration("MainForm")
                                .AddMembers(
                                    SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("void"), "Main")
                                        .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                                        .AddParameterListParameters(parameterList)
                                        .AddTypeParameterListParameters(SyntaxFactory.TypeParameter("T"))
                                        .AddConstraintClauses(SyntaxFactory.TypeParameterConstraintClause("b").AddConstraints())
                                        .WithBody(
                                            SyntaxFactory.Block(
                                                SyntaxFactory.ReturnStatement(
                                                    SyntaxFactory.DefaultExpression(SyntaxFactory.ParseTypeName("int"))))))
                        )
                );

            var output = comp.NormalizeWhitespace().ToFullString();
            Console.Write(output);
            Console.ReadKey();


            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName: "Cake.Intellisense",
                syntaxTrees: new[] { comp.SyntaxTree }
            );


            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    var failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (var diagnostic in failures)
                    {
                        Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    var assembly = Assembly.Load(ms.ToArray());
                }
            }
        }
    }
}