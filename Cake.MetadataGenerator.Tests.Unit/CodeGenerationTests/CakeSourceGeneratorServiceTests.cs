using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Cake.MetadataGenerator.CodeGeneration.SourceGenerators;
using Cake.MetadataGenerator.Compilation;
using Cake.MetadataGenerator.Tests.Unit.Common;
using Cake.MetadataGenerator.Tests.Unit.Extensions;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NSubstitute;
using Xunit;

namespace Cake.MetadataGenerator.Tests.Unit.CodeGenerationTests
{
    public class CakeSourceGeneratorServiceTests
    {
        public class GenerateMethod : Test<CakeSourceGeneratorService>
        {
            public GenerateMethod()
            {
                var compilation = Use<Microsoft.CodeAnalysis.Compilation>(string.Empty, null, null, false, null);
                compilation.SyntaxTrees.Returns(new List<SyntaxTree> { CSharpSyntaxTree.ParseText(string.Empty) });
                compilation.AddSyntaxTrees(Arg.Any<SyntaxTree[]>()).Returns(compilation);
                compilation.ReplaceSyntaxTree(Arg.Any<SyntaxTree>(), Arg.Any<SyntaxTree>()).Returns(compilation);
                Get<ICompilationProvider>().Get(Arg.Any<string>(), references: Arg.Any<IEnumerable<MetadataReference>>()).Returns(compilation);
            }

            [Fact]
            public void RecursivelyIteratesOverNamespaces()
            {
                var rootNamespaceSymbol = Use<INamespaceSymbol>();
                var firstLevelSymbol = Substitute.For<INamespaceSymbol>();
                var secondLevelSymbol = Substitute.For<INamespaceSymbol>();

                rootNamespaceSymbol.GetTypeMembers().Returns(ImmutableArray.Create<INamedTypeSymbol>());
                rootNamespaceSymbol.GetNamespaceMembers().Returns(new[] { firstLevelSymbol });
                firstLevelSymbol.GetNamespaceMembers().Returns(new[] { secondLevelSymbol });
                firstLevelSymbol.GetTypeMembers().Returns(ImmutableArray.Create<INamedTypeSymbol>());
                secondLevelSymbol.GetNamespaceMembers().Returns(Enumerable.Empty<INamespaceSymbol>());
                secondLevelSymbol.GetTypeMembers().Returns(ImmutableArray.Create<INamedTypeSymbol>());
                Get<Microsoft.CodeAnalysis.Compilation>().ProtectedProperty("CommonGlobalNamespace").Returns(rootNamespaceSymbol);

                Subject.Generate(this.GetType().Assembly);

                rootNamespaceSymbol.Received().GetNamespaceMembers();
                firstLevelSymbol.Received().GetNamespaceMembers();
                secondLevelSymbol.Received().GetNamespaceMembers();
            }

            [Fact]
            public void DoesNotIncludeNamespacesWitoutCakeLikeTypes()
            {
                var rootNamespaceSymbol = Use<INamespaceSymbol>();
                rootNamespaceSymbol.GetTypeMembers().Returns(ImmutableArray.Create<INamedTypeSymbol>());
                rootNamespaceSymbol.GetNamespaceMembers().Returns(ImmutableArray.Create<INamespaceSymbol>());
                Get<Microsoft.CodeAnalysis.Compilation>().ProtectedProperty("CommonGlobalNamespace").Returns(rootNamespaceSymbol);

                var result = Subject.Generate(this.GetType().Assembly);

                result.Should().NotBeNull();
                result.Members.OfType<NamespaceDeclarationSyntax>().Should().BeEmpty();
                Get<IMetadataGeneratorService>().DidNotReceive().CreateNamedTypeDeclaration(Arg.Any<INamedTypeSymbol>());
            }

            [Fact]
            public void AddsClassDeclarationSyntax_WhenTypeDecoratedWithCakeAliasCategoryAttribute()
            {
                var cakeSymbol = Substitute.For<INamedTypeSymbol>();
                cakeSymbol.Name.Returns(CakeAttributeNames.CakeAliasCategory);
                var namedTypeSymbol = Use<INamedTypeSymbol>();
                var rootNamespaceSymbol = Use<INamespaceSymbol>();
                var attributeData = Use<AttributeData>();
                attributeData.ProtectedProperty("CommonAttributeClass").Returns(cakeSymbol);
                namedTypeSymbol.Kind.Returns(SymbolKind.NamedType);
                namedTypeSymbol.GetAttributes().Returns(ImmutableArray.Create(attributeData));
                rootNamespaceSymbol.GetTypeMembers().Returns(ImmutableArray.Create(namedTypeSymbol));
                rootNamespaceSymbol.GetNamespaceMembers().Returns(ImmutableArray.Create<INamespaceSymbol>());
                Get<Microsoft.CodeAnalysis.Compilation>().ProtectedProperty("CommonGlobalNamespace").Returns(rootNamespaceSymbol);
                Get<IMetadataGeneratorService>().CreateNamedTypeDeclaration(Arg.Any<INamedTypeSymbol>()).Returns(SyntaxFactory.ClassDeclaration("MyClass"));

                var result = Subject.Generate(this.GetType().Assembly);

                result.Members.OfType<NamespaceDeclarationSyntax>().Should().HaveCount(1);
                result.Members.OfType<NamespaceDeclarationSyntax>()
                    .SelectMany(syntax => syntax.Members)
                    .Should()
                    .ContainSingle(
                        memberSyntax =>
                            memberSyntax.Kind() == SyntaxKind.ClassDeclaration &&
                            ((ClassDeclarationSyntax) memberSyntax).Identifier.ToString() == "MyClass");

                Get<IMetadataGeneratorService>().Received(1).CreateNamedTypeDeclaration(Arg.Is<INamedTypeSymbol>(symbol => symbol == namedTypeSymbol));
            }

            [Fact]
            public void AddsClassDeclarationSyntax_WhenTypeIsCakeEngine()
            {
                var namedTypeSymbol = Use<INamedTypeSymbol>();
                var rootNamespaceSymbol = Use<INamespaceSymbol>();
                namedTypeSymbol.Kind.Returns(SymbolKind.NamedType);
                namedTypeSymbol.Name.Returns(CakeEngineNames.ScriptHost);
                rootNamespaceSymbol.GetTypeMembers().Returns(ImmutableArray.Create(namedTypeSymbol));
                rootNamespaceSymbol.GetNamespaceMembers().Returns(ImmutableArray.Create<INamespaceSymbol>());
                Get<Microsoft.CodeAnalysis.Compilation>().ProtectedProperty("CommonGlobalNamespace").Returns(rootNamespaceSymbol);
                Get<IMetadataGeneratorService>().CreateNamedTypeDeclaration(Arg.Any<INamedTypeSymbol>()).Returns(SyntaxFactory.ClassDeclaration("MyClass"));

                var result = Subject.Generate(this.GetType().Assembly);

                result.Members.OfType<NamespaceDeclarationSyntax>().Should().HaveCount(1);
                result.Members.OfType<NamespaceDeclarationSyntax>()
                    .SelectMany(syntax => syntax.Members)
                    .Should()
                    .ContainSingle(
                        memberSyntax =>
                            memberSyntax.Kind() == SyntaxKind.ClassDeclaration &&
                            ((ClassDeclarationSyntax)memberSyntax).Identifier.ToString() == "MyClass");

                Get<IMetadataGeneratorService>().Received(1).CreateNamedTypeDeclaration(Arg.Is<INamedTypeSymbol>(symbol => symbol == namedTypeSymbol));
            }
        }
    }
}