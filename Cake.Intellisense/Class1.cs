using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
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
                                    SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("int"), "Main")
                                        .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                                        .AddParameterListParameters(parameterList)
                                        .AddTypeParameterListParameters(SyntaxFactory.TypeParameter("T"))
                                        .AddConstraintClauses(SyntaxFactory.TypeParameterConstraintClause("T").AddConstraints(SyntaxFactory.TypeConstraint(SyntaxFactory.ParseTypeName("class"))))
                                        .WithBody(
                                            SyntaxFactory.Block(
                                                SyntaxFactory.ReturnStatement(
                                                    SyntaxFactory.DefaultExpression(SyntaxFactory.ParseTypeName("int"))))))
                        )
                ).NormalizeWhitespace();


            MetadataReference[] references =
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location)
            };

            var x = comp.ToFullString();

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName: "Cake.Intellisense",
                syntaxTrees: new[] { CSharpSyntaxTree.Create(comp) },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
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