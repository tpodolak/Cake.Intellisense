using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Cake.Core.Annotations;
using Cake.Core.Scripting;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
namespace Cake.Intellisense
{
    public class Class1
    {
        private static XDocument documentation = XDocument.Load("Cake.Common.xml");

        private static Type[] excludedAttributes = new[]
        {
            typeof(ParamArrayAttribute), typeof(ExtensionAttribute), typeof(CakeAliasCategoryAttribute),
            typeof(CakeNamespaceImportAttribute), typeof(CakeMethodAliasAttribute)
        };


        //        public void Foo()
        //        {
        //
        //            var parameterList = new[]
        //            {
        //                Parameter(Identifier("name")).WithType(ParseTypeName("int"))
        //            };
        //
        //            var comp = CompilationUnit()
        //                .AddMembers(
        //                    NamespaceDeclaration(IdentifierName("ACO"))
        //                        .AddMembers(
        //                            ClassDeclaration("MainForm")
        //                                .AddMembers(
        //                                    MethodDeclaration(ParseTypeName("int"), "Main")
        //                                        .AddModifiers(Token(SyntaxKind.PublicKeyword))
        //                                        .AddParameterListParameters(parameterList)
        //                                        .AddTypeParameterListParameters(TypeParameter("T"))
        //                                        .AddConstraintClauses(TypeParameterConstraintClause("T").AddConstraints(TypeConstraint(ParseTypeName("class"))))
        //                                        .WithBody(
        //                                            Block(
        //                                                ReturnStatement(
        //                                                    DefaultExpression(ParseTypeName("int"))))))
        //                        )
        //                ).NormalizeWhitespace();
        //
        //
        //            MetadataReference[] references =
        //            {
        //                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        //                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location)
        //            };
        //
        //            var x = comp.ToFullString();
        //
        //            CSharpCompilation compilation = CSharpCompilation.Create(
        //                assemblyName: "Cake.Intellisense",
        //                syntaxTrees: new[] { CSharpSyntaxTree.Create(comp) },
        //                references: references,
        //                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        //            );
        //
        //
        //            Compile(compilation);
        //        }

        public void Compile(CSharpCompilation compilation, string output)
        {
            EmitResult result = compilation.Emit(output, null, Path.ChangeExtension(output, "xml"));

            if (!result.Success)
            {
                var failures = result.Diagnostics.Where(diagnostic =>
                    diagnostic.IsWarningAsError ||
                    diagnostic.Severity == DiagnosticSeverity.Error);

                foreach (var diagnostic in failures)
                {
                    Console.Error.WriteLine(diagnostic);
                }
            }
        }

        public CompilationUnitSyntax Genrate(List<Type> types)
        {
            var cu = CompilationUnit();
            foreach (var grouping in types.GroupBy(val => val.Namespace))
            {
                cu = cu.AddMembers(CreateNamespace(grouping.Key).AddMembers(grouping.Select(CreateClass).ToArray()));
            }
            return cu.NormalizeWhitespace();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public MemberDeclarationSyntax CreateMethodDeclaration(MethodInfo methodInfo)
        {
            var methodDeclarationSyntax = MethodDeclaration(ParseTypeName(PrettyTypeName(methodInfo.ReturnType)), methodInfo.Name)
                .AddModifiers(CreateModifiers(methodInfo).ToArray())
                .AddParameterListParameters(CreateParameters(methodInfo))
                .AddConstraintClauses(CreateConstraintClauses(methodInfo))
                .WithBody(CreateBody(methodInfo));

            if (methodInfo.IsGenericMethod)
                methodDeclarationSyntax =
                    methodDeclarationSyntax.AddTypeParameterListParameters(CreateTypeParameters(methodInfo));

            if (methodInfo.GetCustomAttributes(true).Select(val => val.GetType()).Except(excludedAttributes).Any())
                methodDeclarationSyntax = methodDeclarationSyntax.AddAttributeLists(CreateAttributes(methodInfo));

            return methodDeclarationSyntax;
        }

        public AttributeListSyntax[] CreateAttributes(MethodInfo methodInfo)
        {
            var res =
                methodInfo.GetCustomAttributesData()
                    .Where(val => !excludedAttributes.Contains(val.AttributeType))
                    .Select(val => CreateAttribute(val));

            return new[] { AttributeList(SeparatedList(res)) };
        }

        private static AttributeSyntax CreateAttribute(CustomAttributeData data)
        {
            var props = data.ConstructorArguments.Select(x => x.Value).Select(val => AttributeArgument(IdentifierName(val is string ? $"\"{val.ToString()}\"" : val.ToString().ToLower())));

            return Attribute(IdentifierName(PrettyTypeName(data.AttributeType))).WithArgumentList(AttributeArgumentList(SeparatedList(props)));
        }

        public ParameterSyntax[] CreateParameters(MethodInfo methodInfo)
        {
            var skip = methodInfo.IsDefined(typeof(ExtensionAttribute), true) ? 1 : 0;
            return methodInfo.GetParameters().Skip(skip).Select(CreateParameter).ToArray();
        }

        private static ParameterSyntax CreateParameter(ParameterInfo val)
        {
            var modifiers = new SyntaxTokenList();

            if (val.GetCustomAttributes(typeof(ParamArrayAttribute), false).Length > 0)
                modifiers = modifiers.Add(Token(SyntaxKind.ParamsKeyword));

            if (val.IsOut)
                modifiers = modifiers.Add(Token(SyntaxKind.OutKeyword));
            else if (val.ParameterType.IsByRef)
                modifiers = modifiers.Add(Token(SyntaxKind.RefKeyword));

            var parameterSyntax = Parameter(Identifier(val.Name))
                                               .WithType(ParseTypeName(PrettyTypeName(val.ParameterType)))
                                               .WithModifiers(modifiers);

            return parameterSyntax;
        }

        public BlockSyntax CreateBody(MethodInfo methodInfo)
        {
            var statements = new SyntaxList<StatementSyntax>();

            statements = statements.AddRange(methodInfo.GetParameters().Where(val => val.IsOut).Select(val => ExpressionStatement(
                 AssignmentExpression(
                     SyntaxKind.SimpleAssignmentExpression,
                     IdentifierName(val.Name),
                     DefaultExpression(ParseTypeName(PrettyTypeName(val.ParameterType)))))));

            if (methodInfo.ReturnType != typeof(void))
            {
                statements = statements.Add(ReturnStatement(
                    DefaultExpression(ParseTypeName(PrettyTypeName(methodInfo.ReturnType)))));
            }

            var block = Block(statements);
            return block;
        }

        public IEnumerable<SyntaxToken> CreateModifiers(MethodInfo methodInfo)
        {
            string path = "M:" + methodInfo.DeclaringType.FullName + "." + methodInfo.Name;

            var res = @" /// <summary>
        /// 
        /// </summary>";

            //            var xmlDocuOfMethod = documentation.XPathSelectElement("//member[starts-with(@name, '" + path + "')]");
            //
            //            var doc = documentation.Root.Elements("members").Elements().FirstOrDefault(el => el.Attribute("name")?.Value.StartsWith(path) == true);
            //
            //            var nodes = doc.Descendants().Where(val => val.Name != "param" || val.Attribute("name")?.Value != methodInfo.GetParameters().First().Name);
            //            var result = string.Join(Environment.NewLine, nodes.Select(val => val.ToString()));
            //
            //            var res2 = string.Join(Environment.NewLine, result.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(val => @"///" + val));
            //
            //            SyntaxTrivia comment = Comment(res);
            //
            //            yield return Token(TriviaList(comment), SyntaxKind.None, TriviaList(CarriageReturn));

            yield return Token(SyntaxKind.PublicKeyword);

            yield return Token(SyntaxKind.StaticKeyword);

        }

        public TypeParameterConstraintClauseSyntax[] CreateConstraintClauses(MethodInfo methodInfo)
        {
            return new TypeParameterConstraintClauseSyntax[0];
            return new[]
            {
                TypeParameterConstraintClause("T")
                    .AddConstraints(
                        TypeConstraint(ParseTypeName("class")))
            };
        }

        public NamespaceDeclarationSyntax CreateNamespace(string @namespace)
        {
            return NamespaceDeclaration(IdentifierName(@namespace));
        }

        public TypeParameterSyntax[] CreateTypeParameters(MethodInfo methodInfo)
        {
            if (methodInfo.IsGenericMethod)
            {
                return methodInfo.GetGenericArguments().Select(val => TypeParameter(PrettyTypeName(val))).ToArray();
            }

            return new TypeParameterSyntax[0];
        }

        public ClassDeclarationSyntax CreateClass(Type type)
        {
            return
                ClassDeclaration(type.Name + "Metadata").WithModifiers(SyntaxTokenList.Create(Token(SyntaxKind.PublicKeyword)))
                    .AddMembers(
                        type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                            .Where(val => val.GetCustomAttributes<CakeMethodAliasAttribute>().Any() || val.DeclaringType?.FullName == typeof(ScriptHost).FullName)
                            .Select(CreateMethodDeclaration)
                            .ToArray());
        }

        static string PrettyTypeName(Type t)
        {
            var x = !string.IsNullOrWhiteSpace(t.FullName) ? $"global::{t.Namespace}" + "." : string.Empty;
            var name = t.Name.TrimEnd('&');
            if (t.GetGenericArguments().Length > 0)
            {
                return $"{x + name.Substring(0, name.LastIndexOf("`", StringComparison.InvariantCulture))}<{string.Join(", ", t.GetGenericArguments().Select(PrettyTypeName))}>";
            }

            return t == typeof(void) ? "void" : x + name;
        }
    }
}