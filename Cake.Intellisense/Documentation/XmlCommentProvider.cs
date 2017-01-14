using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
namespace Cake.MetadataGenerator.Documentation
{
    public class XmlCommentProvider : IDocumentationProvider
    {
        public string Get(XDocument documentation, string commentId)
        {
            var commentSection = documentation.XPathSelectElement("//member[starts-with(@name, '" + commentId + "')]");

//            using (var sw = new System.IO.StringWriter())
//            {
//                using (var xw = new System.Xml.XmlTextWriter(sw))
//                {
//                    xw.Formatting = System.Xml.Formatting.Indented;
//                    xw.Indentation = 4;
//                    commentSection.Save(xw);
//                }
//                return sw.ToString();
//            }
            // var nodes = result.Descendants().Where(val => val.Name != "param" || val.Attribute("name")?.Value != methodInfo.GetParameters().First().Name);
            var result = string.Join(Environment.NewLine, commentSection.Nodes().Select(val => val.ToString()));

            var res2 = string.Join(Environment.NewLine, result.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(val => @"/// " + val));
            return res2;
        }
    }

    public interface IDocumentationProvider
    {
        string Get(XDocument documentation, string commentId);
    }
}