using System.Collections.Immutable;
using System.Xml.Linq;
using Cake.MetadataGenerator.Documentation;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using NSubstitute;
using Xunit;

namespace Cake.MetadataGenerator.Tests.Unit.DocumentationTests
{
    public class XmlCommentProviderTests : Test<XmlCommentProvider>
    {
        private readonly XDocument emptyDocument = XDocument.Parse("<?xml version=\"1.0\"?><doc></doc>");
        private readonly XDocument validDocumet = XDocument.Parse(@"<?xml version=""1.0""?>
<doc>
    <assembly>
        <name>Cake.Common</name>
    </assembly>
    <members>
        <member name=""M:Cake.Common.ArgumentAliases.Argument``1(Cake.Core.ICakeContext,System.String,``0)"">
            <summary>
                Gets an argument and returns the provided
                <paramref name=""defaultValue"" /> if the argument is missing.
            </summary>
            <typeparam name=""T"">The argument type.</typeparam>
            <param name=""context"">The context.</param>
            <param name=""name"">The argument name.</param>
            <param name=""defaultValue"">The value to return if the argument is missing.</param>
            <returns>The value of the argument if it exist; otherwise
                <paramref name=""defaultValue"" />.</returns>
            <example>
                <code>
            //Cake.exe .\argument.cake -myArgument=""is valid"" -loopCount = 5
            Information(""Argument {0}"", Argument&lt;string&gt;(""myArgument"", ""is NOT valid""));
            var loopCount = Argument&lt;int&gt;(""loopCount"", 10);
            for(var index = 0;index&lt;loopCount; index++)
            {
                Information(""Index {0}"", index);
            }
            </code>
            </example>
        </member>
    </members>
</doc>", LoadOptions.PreserveWhitespace);
        public XmlCommentProviderTests()
        {
            Use<ISymbol>();
        }

        [Fact]
        public void GetReturnsEmptyStringWhenSectionForGivenCommentIdDoesNotExist()
        {
            FakeOf<ISymbol>().GetDocumentationCommentId().Returns(string.Empty);

            var result = Subject.Get(emptyDocument, FakeOf<ISymbol>());

            result.Should().BeEmpty();
        }

        [Fact]
        public void GeRetunsValidCSharpCommentBasedOnXmlDocumentation()
        {
            FakeOf<ISymbol>().GetDocumentationCommentId().Returns(@"M:Cake.Common.ArgumentAliases.Argument``1(Cake.Core.ICakeContext,System.String,``0)");

            var result = Subject.Get(validDocumet, FakeOf<ISymbol>());

            result.Should().NotBeNull();
            result.Should().Be(@"///             <summary>
///                 Gets an argument and returns the provided
///                 <paramref name=""defaultValue"" /> if the argument is missing.
///             </summary>
///             <typeparam name=""T"">The argument type.</typeparam>
///             <param name=""context"">The context.</param>
///             <param name=""name"">The argument name.</param>
///             <param name=""defaultValue"">The value to return if the argument is missing.</param>
///             <returns>The value of the argument if it exist; otherwise
///                 <paramref name=""defaultValue"" />.</returns>
///             <example>
///                 <code>
///             //Cake.exe .\argument.cake -myArgument=""is valid"" -loopCount = 5
///             Information(""Argument {0}"", Argument&lt;string&gt;(""myArgument"", ""is NOT valid""));
///             var loopCount = Argument&lt;int&gt;(""loopCount"", 10);
///             for(var index = 0;index&lt;loopCount; index++)
///             {
///                 Information(""Index {0}"", index);
///             }
///             </code>
///             </example>
///         ");
        }

        [Theory]
        [InlineData(CakeAttributes.CakePropertyAlias)]
        [InlineData(CakeAttributes.CakeMethodAlias)]
        public void GetRetursalidCSharpCommentWitFirstParamRemovedWhenSymbolIsMethodDecoratedWithCakeAttributes(string methodAttribute)
        {
            var attributeData = Use<AttributeData>();
            var namedTypeSymbol = Use<INamedTypeSymbol>();
            var parameterSymbol = Use<IParameterSymbol>();
            var methodSymbol = Use<IMethodSymbol>();

            parameterSymbol.Name.Returns("context");
            namedTypeSymbol.Name.Returns(methodAttribute);
            attributeData.AttributeClass.Returns(namedTypeSymbol);
            methodSymbol.Kind.Returns(SymbolKind.Method);
            methodSymbol.Parameters.Returns(ImmutableArray.Create(parameterSymbol));

            methodSymbol.GetDocumentationCommentId().Returns(@"M:Cake.Common.ArgumentAliases.Argument``1(Cake.Core.ICakeContext,System.String,``0)");
            methodSymbol.Kind.Returns(SymbolKind.Method);
            methodSymbol.GetAttributes().Returns(ImmutableArray.Create(attributeData));

            var result = Subject.Get(validDocumet, FakeOf<IMethodSymbol>());

            result.Should().NotBeNull();
            result.Should().Be(@"///             <summary>
///                 Gets an argument and returns the provided
///                 <paramref name=""defaultValue"" /> if the argument is missing.
///             </summary>
///             <typeparam name=""T"">The argument type.</typeparam>
///             
///             <param name=""name"">The argument name.</param>
///             <param name=""defaultValue"">The value to return if the argument is missing.</param>
///             <returns>The value of the argument if it exist; otherwise
///                 <paramref name=""defaultValue"" />.</returns>
///             <example>
///                 <code>
///             //Cake.exe .\argument.cake -myArgument=""is valid"" -loopCount = 5
///             Information(""Argument {0}"", Argument&lt;string&gt;(""myArgument"", ""is NOT valid""));
///             var loopCount = Argument&lt;int&gt;(""loopCount"", 10);
///             for(var index = 0;index&lt;loopCount; index++)
///             {
///                 Information(""Index {0}"", index);
///             }
///             </code>
///             </example>
///         ");
        }
    }
}