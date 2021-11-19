using NUnit.Framework;
using KeyCipherLib;
using NUnit.Framework.Internal;
using System;
using System.Text;

namespace Tests
{
    public class Tests
    {
        ICipher cipher;
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void MatrixCipherEncodeDecodeTest()
        {
            cipher = new MatrixProductionCipher(new double[,] { { 3, 2, 3 },
                                                                { 4, 5, 6 },
                                                                { 7, 8, 9 } });
            Random rand = new Random();
            string testText;
            for (int i = 0; i < 10000; i++)
            {
                testText = GetRandomString(rand.Next(5, 100));
                string encodedText = cipher.Encode(testText);
                string decodedText = cipher.Decode(encodedText);
                Assert.AreEqual(testText, decodedText);
            }
        }
        private string GetRandomString(int length)
        {
            Random rand = new Random();
            StringBuilder builder = new StringBuilder();
            for(int i = 0; i < length; i++)
            {
                builder.Append((char)rand.Next(1, 1000));
            }
            return builder.ToString();
        }
    }
}