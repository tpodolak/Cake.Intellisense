using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Cake.Core.Annotations;
using Cake.Core.Scripting;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;
using NLog;

namespace Cake.MetadataGenerator
{
    public class Class1
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static Type[] excludedAttributes = new[]
        {
            typeof(ParamArrayAttribute), typeof(ExtensionAttribute), typeof(CakeAliasCategoryAttribute),
            typeof(CakeNamespaceImportAttribute), typeof(CakeMethodAliasAttribute)
        };

        public Assembly Compile(CSharpCompilation compilation, string output)
        {
            var res = compilation.GetMetadataReference(compilation.Assembly);
            var result = compilation.Emit(output, null, Path.ChangeExtension(output, "xml"));

            if (!result.Success)
            {
                var failures = result.Diagnostics.Where(diagnostic =>
                    diagnostic.IsWarningAsError ||
                    diagnostic.Severity == DiagnosticSeverity.Error);

                foreach (var diagnostic in failures)
                {
                    Logger.Error(diagnostic);
                }

                return null;
            }

            return Assembly.LoadFrom(output);
        }

        public CompilationUnitSyntax Generate(CSharpCompilation original)
        {
            var cu = SyntaxFactory.CompilationUnit();


            var symbols = original.GlobalNamespace.GetNamespaceMembers().ToList();
            var member = symbols[3].GetMembers().ToList()[1].GetTypeMembers()[4].GetMembers()[0];
            

            return null;
        }

        public CompilationUnitSyntax Genrate(List<Type> types)
        {
            var cu = SyntaxFactory.CompilationUnit();
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
            var methodDeclarationSyntax = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName(PrettyTypeName(methodInfo.ReturnType)), methodInfo.Name)
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

            return new[] { SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList(res)) };
        }

        private static AttributeSyntax CreateAttribute(CustomAttributeData data)
        {
            var props = data.ConstructorArguments.Select(x => x.Value).Select(val => SyntaxFactory.AttributeArgument(SyntaxFactory.IdentifierName(val is string ? $"\"{val.ToString()}\"" : val.ToString().ToLower())));

            return SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(PrettyTypeName(data.AttributeType))).WithArgumentList(SyntaxFactory.AttributeArgumentList(SyntaxFactory.SeparatedList(props)));
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
                modifiers = modifiers.Add(SyntaxFactory.Token(SyntaxKind.ParamsKeyword));

            if (val.IsOut)
                modifiers = modifiers.Add(SyntaxFactory.Token(SyntaxKind.OutKeyword));
            else if (val.ParameterType.IsByRef)
                modifiers = modifiers.Add(SyntaxFactory.Token(SyntaxKind.RefKeyword));

            var parameterSyntax = SyntaxFactory.Parameter(SyntaxFactory.Identifier(val.Name))
                                               .WithType(SyntaxFactory.ParseTypeName(PrettyTypeName(val.ParameterType)))
                                               .WithModifiers(modifiers);

            return parameterSyntax;
        }

        public BlockSyntax CreateBody(MethodInfo methodInfo)
        {
            var statements = new SyntaxList<StatementSyntax>();

            statements = statements.AddRange(methodInfo.GetParameters().Where(val => val.IsOut).Select(val => SyntaxFactory.ExpressionStatement(
                 SyntaxFactory.AssignmentExpression(
                     SyntaxKind.SimpleAssignmentExpression,
                     SyntaxFactory.IdentifierName(val.Name),
                     SyntaxFactory.DefaultExpression(SyntaxFactory.ParseTypeName(PrettyTypeName(val.ParameterType)))))));

            if (methodInfo.ReturnType != typeof(void))
            {
                statements = statements.Add(SyntaxFactory.ReturnStatement(
                    SyntaxFactory.DefaultExpression(SyntaxFactory.ParseTypeName(PrettyTypeName(methodInfo.ReturnType)))));
            }

            var block = SyntaxFactory.Block(statements);
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

            yield return SyntaxFactory.Token(SyntaxKind.PublicKeyword);

            yield return SyntaxFactory.Token(SyntaxKind.StaticKeyword);

        }

        public TypeParameterConstraintClauseSyntax[] CreateConstraintClauses(MethodInfo methodInfo)
        {
            return new TypeParameterConstraintClauseSyntax[0];
            return new[]
            {
                SyntaxFactory.TypeParameterConstraintClause("T")
                    .AddConstraints(
                        SyntaxFactory.TypeConstraint(SyntaxFactory.ParseTypeName("class")))
            };
        }

        public NamespaceDeclarationSyntax CreateNamespace(string @namespace)
        {
            return SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName(@namespace));
        }

        public TypeParameterSyntax[] CreateTypeParameters(MethodInfo methodInfo)
        {
            if (methodInfo.IsGenericMethod)
            {
                return methodInfo.GetGenericArguments().Select(val => SyntaxFactory.TypeParameter(PrettyTypeName(val))).ToArray();
            }

            return new TypeParameterSyntax[0];
        }

        public ClassDeclarationSyntax CreateClass(Type type)
        {
            return
                SyntaxFactory.ClassDeclaration(type.Name + "Metadata").WithModifiers(SyntaxTokenList.Create(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                    .AddMembers(
                        type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                            .Where(val => val.GetCustomAttributes<CakeMethodAliasAttribute>().Any() || (val.DeclaringType?.FullName == typeof(ScriptHost).FullName && !val.IsSpecialName))
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