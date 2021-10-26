using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StreamDecorators;

namespace UnitTests
{
    [TestClass]
    public class BufferStreamDecorationTest
    {
        [TestMethod]
        public void ReadDecoratingTest()
        {
            //Arange
            int result = 0;
            //Act
            int num = 2;
            byte[] array = new byte[256];
            using (var stream = new BufferedStream(new MemoryStream(array)))
            {
                CalcStreamDecorator decoratedStream = new CalcStreamDecorator(Convert.ToInt32(num), stream);
                decoratedStream.Read(array, 0, array.Length);
                result = decoratedStream.Values.Count;
                Console.WriteLine(array[0]);
            }
            int expected = 2;
            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
