using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StreamDecorators;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class FileStreamDecorationTest
    {
        [TestMethod]
        public void ReadDecoratingTest()
        {
            //Arange
            int result = 0;
            //Act
            string filePath = "UnitTest.txt";
            int num = 2;
            byte[] array = new byte[256];
            using (var stream = File.OpenRead(filePath))
            {
                CalcStreamDecorator decoratedStream = new CalcStreamDecorator(num, stream);
                decoratedStream.Read(array, 0, array.Length);
                result = decoratedStream.Values.Count;     
            }
            int expected = 2;
            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
