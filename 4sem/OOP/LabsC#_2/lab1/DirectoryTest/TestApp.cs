using Microsoft.VisualStudio.TestTools.UnitTesting;
using DirectoryOrganizations;
using System.Collections.Generic;

namespace DirectoryTest
{
    [TestClass]
    public class TestApp
    {
        [TestMethod]
        public void TestValidation()
        {
            //Arange
            string path = (@"C:\Users\nikit\Desktop\OOP\LabsC#_2\lab1\Lab1\directory.xml");
            string schemePaath = (@"C:\Users\nikit\Desktop\OOP\LabsC#_2\lab1\Lab1\scheme.xsd");

            bool expected = true;
            //Act
            bool result = XMLValidator.ValidateXml(path, schemePaath);
            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]

        public void TestValidationTwo()
        {
            //Arange
            string path = (@"C:\Users\nikit\Desktop\OOP\LabsC#_2\lab1\Lab1\ppp.xml");
            string schemePaath = (@"C:\Users\nikit\Desktop\OOP\LabsC#_2\lab1\Lab1\scheme.xsd");

            bool expected = false;
            //Act
            bool result = XMLValidator.ValidateXml(path, schemePaath);
            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]

        public void TestReadFileThree()
        {
            //Arange
            string path = (@"C:\Users\nikit\Desktop\OOP\LabsC#_2\lab1\Lab1\directory.xml");
            var record = new Records("Zelenstoi", "Private", "Gomel, Lipeckaja 18 street", "80297672336", "123");
            Directory directory = new Directory();
            directory.DirectoryList.Add(record);
            //Act
            var result = XMLReader.ReadXML(path).DirectoryList[0];
            //Assert
            Assert.AreEqual(record, result);
        }


/*        [TestMethod]

        public void TestWriteFile()
        {
            //Arange
            string path = (@"C:\Users\nikit\Desktop\OOP\LabsC#_2\lab1\Lab1\unitTest.xml");
            int expected = 2;

            //Act
            Directory directory = new Directory();
            var record = new Records("Zelenstoi", "Private", "Gomel, Lipeckaja 18 street", "80297672336", "123");
            directory.DirectoryList.Add(record);
            var recordTwo = new Records("Zelenstodasi", "Privadaste", "Gomeasl, Lipeckadasja 18 street", "80222297672336", "122223");
            directory.DirectoryList.Add(recordTwo);
            XMLWriter.WriteXML(directory, path);
            XMLReader.ReadXML(path);
            int result = directory.DirectoryList.Count;
            //Assert
            Assert.AreEqual(expected,result);
        }*/




    }
}
