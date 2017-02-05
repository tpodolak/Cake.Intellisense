using Cake.MetadataGenerator.Documentation;
using Cake.MetadataGenerator.FileSystem;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Cake.MetadataGenerator.Tests.Unit.DocumentationTests
{
    public class DocumentationReaderTests : Test<DocumentationReader>
    {
        [Fact]
        public void ReadReturnsEmptyDocumentationWhenFileDoesNotExist()
        {
            FakeOf<IFileSystem>().FileExists(Arg.Any<string>()).Returns(false);

            var result = Subject.Read("path");

            result.Should().NotBeNull();
            result.Root.Should().NotBeNull();
            result.Root.DescendantNodes().Should().BeEmpty();
            FakeOf<IFileSystem>().DidNotReceive().ReadAllText(Arg.Any<string>());
        }

        [Fact]
        public void ReadReturnsParsedDocumentationWhenFileExists()
        {
            FakeOf<IFileSystem>().FileExists(Arg.Any<string>()).Returns(true);
            FakeOf<IFileSystem>().ReadAllText(Arg.Any<string>()).Returns(@"<?xml version=""1.0""?>
                                                                                <doc>
                                                                                    <assembly>
                                                                                        <name>Cake.Core.Metadata</name>
                                                                                    </assembly>
                                                                                </doc>");

            var result = Subject.Read("path");

            result.Should().NotBeNull();
            result.Root.DescendantNodes().Should().NotBeNullOrEmpty();
            FakeOf<IFileSystem>().Received(1).ReadAllText(Arg.Any<string>());
        }
    }
}