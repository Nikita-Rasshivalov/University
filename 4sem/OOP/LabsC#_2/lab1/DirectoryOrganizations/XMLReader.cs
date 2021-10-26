using System;
using System.Xml;

namespace DirectoryOrganizations
{
    /// <summary>
    /// XMLReade clsaa
    /// </summary>
    public class XMLReader
    {
        /// <summary>
        /// Read XML file
        /// </summary>
        /// <param name="directory">List of records</param>
        public static Directory ReadXML(string path)
        {
            Directory directory = new Directory();
            XmlDocument xml = new XmlDocument();
            xml.Load(path);

            foreach (XmlElement node in xml.DocumentElement)
            {
                string organizationName = node.ChildNodes[0].InnerText;
                string organizationType = node.ChildNodes[1].InnerText;
                string adress = node.ChildNodes[2].InnerText;
                string phoneNumber = node.ChildNodes[3].InnerText;
                string emloyNumbers = node.ChildNodes[4].InnerText;
                Records record = new Records(organizationName, organizationType, adress, phoneNumber, emloyNumbers);
                directory.DirectoryList.Add(record);
            }
            return directory;
        }
    }
}
