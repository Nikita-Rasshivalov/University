using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace DirectoryOrganizations
{
    /// <summary>
    /// XMLWriter class
    /// </summary>
    public class XMLWriter
    {
        public void Serializer()
        {

            SerializabledClass myClass = new SerializabledClass();
            XmlSerializer formatter = new XmlSerializer(typeof(SerializabledClass));

            using (FileStream fs = new FileStream("myClass.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, myClass);

                Debug.WriteLine("Объект сериализован");
            }


            using (FileStream fs = new FileStream("myClass.xml", FileMode.OpenOrCreate))
            {
                SerializabledClass newMyClass = (SerializabledClass)formatter.Deserialize(fs);

                Debug.WriteLine("Объект десериализован");
                
            }
        }
    }
}

