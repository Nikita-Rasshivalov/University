using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Encryption;

namespace EncryptionTests
{
    [TestClass]
    public class FeistelTest
    {
        [TestMethod]
        public void EncodeTest()
        {
            string message = "qwertyqwertyoppo";
            string key = "qwertyui";
            string expected = "ъ'qо\"vы?tя)";
            Assert.AreEqual(expected, Feistel.Encode(message, key)); 
        }
        [TestMethod]
        public void DencodeTest()
        {
            string message = "ъ'qо\"vы?tя)";
            string key = "qwertyui";
            string expected = "qwertyqwertyoppo";
            Assert.AreEqual(expected, Feistel.Dencode(message, key));
        }
        [TestMethod]
        public void EncodeTest2()
        {
            string message = "ahfdsdgfdstdrfgrser";
            string key = "zxcvbnmj";
            string expected = "ЯЛ ГНЗ!БЮЙ ЧИЬ3Б¬ЇGҐЯК5Ґ";
            Assert.AreEqual(expected, Feistel.Encode(message, key));
        }
        [TestMethod]
        public void DencodeTest2()
        {
            string message = "ЯЛ ГНЗ!БЮЙ ЧИЬ3Б¬ЇGҐЯК5Ґ";
            string key = "zxcvbnmj";
            string expected = "ahfdsdgfdstdrfgrser\0\0\0\0\0";
            Assert.AreEqual(expected, Feistel.Dencode(message, key));
        }
    }
}
