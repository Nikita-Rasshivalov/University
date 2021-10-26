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

       


    }
}
