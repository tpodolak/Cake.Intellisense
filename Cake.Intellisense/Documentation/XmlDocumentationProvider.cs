namespace Cake.MetadataGenerator.Documentation
{
    public class XmlDocumentationProvider : IDocumentationProvider
    {
        public XmlDocumentationProvider(IDocumentationReader reader)
        {
        }

        public string Get(string commentId)
        {
            return @"
                    /// <summary>
                    /// 
                    /// </summary>
                    /// <typeparam name=""T""></typeparam>
                    /// <param name=""name""></param>
                    /// <returns></returns>";
        }
    }

    public interface IDocumentationProvider
    {
        string Get(string commentId);
    }
}