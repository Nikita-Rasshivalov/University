using System;
using System.Collections.Generic;
using System.Text;

namespace СipherLibrary
{
    public interface CipherStrategy
    {
        public string Encrypt(string textToEncrypt);
        public string Decrypt(string textToDecrypt);
    }
}
