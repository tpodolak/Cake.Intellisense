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
    }
}