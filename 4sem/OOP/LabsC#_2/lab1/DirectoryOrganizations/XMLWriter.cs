using System;
using System.Xml;

namespace DirectoryOrganizations
{
    /// <summary>
    /// XMLWriter class
    /// </summary>
    public class XMLWriter
    {
        /// <summary>
        /// Add node
        /// </summary>
        /// <param name="childName">name of child node</param>
        /// <param name="childText">child text</param>
        /// <param name="parentNode">parentNode</param>
        /// <param name="doc">general node</param>
        private static void AddChildNode(string childName, string childText, XmlElement parentNode, XmlDocument doc)
        {
            var child = doc.CreateElement(childName);
            child.InnerText = childText;
            parentNode.AppendChild(child);
        }
        /// <summary>
        /// Write in xml file
        /// </summary>
        /// <param name="directory">List of records</param>
        public static void WriteXML(Directory directory, string path)
        {
            var doc = new XmlDocument();
            var root = doc.CreateElement("Directory");
            var xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(xmlDeclaration);
            

            foreach (var dir in directory.DirectoryList)
            {
                var recordNode = doc.CreateElement("Record");
                AddChildNode("OrganizationName", dir.OrganizationName, recordNode, doc);
                AddChildNode("OrganizationType", dir.OrganizationType, recordNode, doc);
                AddChildNode("Adress", dir.Adress, recordNode, doc);
                AddChildNode("PhoneNumber", dir.PhoneNumber, recordNode, doc);
                AddChildNode("EmployNumbers", dir.EmployNumbers, recordNode, doc);
                root.AppendChild(recordNode);
            }
            doc.AppendChild(root);
            doc.Save(path);
        }
    }
}
