using System.Xml.Linq;
using Cake.MetadataGenerator.Documentation;
using Cake.MetadataGenerator.FileSystem;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Cake.MetadataGenerator.Tests.Unit.DocumentationTests
{
    public partial class DocumentationReaderTests
    {
        public class ReadMethod : Test<DocumentationReader>
        {

            [Fact]
            public void ReturnsEmptyDocumentationWhenFileDoesNotExist()
            {
                Get<IFileSystem>().FileExists(Arg.Any<string>()).Returns(false);

                var result = Subject.Read("path");

                result.Should().NotBeNull();
                result.Root.Should().NotBeNull();
                result.Root.DescendantNodes().Should().BeEmpty();
                Get<IFileSystem>().DidNotReceive().ReadAllText(Arg.Any<string>());
            }

            [Fact]
            public void ReturnsParsedDocumentationWhenFileExists()
            {
                Get<IFileSystem>().FileExists(Arg.Any<string>()).Returns(true);
                Get<IFileSystem>().ReadAllText(Arg.Any<string>()).Returns(@"<?xml version=""1.0""?>
                                                                                <doc>
                                                                                    <assembly>
                                                                                        <name>Cake.Core.Metadata</name>
                                                                                    </assembly>
                                                                                </doc>");

                var result = Subject.Read("path");

                result.Should().NotBeNull();
                result.Root.DescendantNodes().Should().NotBeNullOrEmpty();
                Get<IFileSystem>().Received(1).ReadAllText(Arg.Any<string>());
            }

            [Fact]
            public void ReturnsParsedDocumentationWithPreservedWhitespaces()
            {
                var whiteSpacedDoc = @"<?xml version=""1.0""?>
                                    <doc>
                                        <assembly>
                                                 <name> Cake.Core.Metadata 
                                        </name>
                                        </assembly>
                                    </doc>";
                Get<IFileSystem>().FileExists(Arg.Any<string>()).Returns(true);
                Get<IFileSystem>().ReadAllText(Arg.Any<string>()).Returns(whiteSpacedDoc);

                var result = Subject.Read("path");

                result.ToString(SaveOptions.DisableFormatting).Should().Be(XDocument.Parse(whiteSpacedDoc, LoadOptions.PreserveWhitespace).ToString(SaveOptions.DisableFormatting));
            }
        }
    }
}