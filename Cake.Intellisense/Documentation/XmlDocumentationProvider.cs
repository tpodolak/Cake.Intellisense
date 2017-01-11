namespace Cake.MetadataGenerator.Documentation
{
    public class XmlDocumentationProvider : IDocumentationProvider
    {
        public XmlDocumentationProvider(IDocumentationReader reader)
        {
        }

        public string Get(string commentId)
        {
            return null;
        }
    }

    public interface IDocumentationProvider
    {
        string Get(string commentId);
    }
}