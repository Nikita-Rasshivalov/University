using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StreamDecorators;

namespace UnitTests
{
    [TestClass]
    public class MemoryStreamDecorationTest
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

            
            using (var stream = new MemoryStream(array))
            {
                
                CalcStreamDecorator decoratedStream = new CalcStreamDecorator(num, stream);
                byte[] array2 = new byte[256];
                decoratedStream.Read(array2, 0, array.Length);
                result = decoratedStream.Values.Count;
                Console.WriteLine(array2[0]);
            }
            int expected = 2;
            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
