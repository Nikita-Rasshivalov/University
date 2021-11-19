using System;

namespace СipherLibrary
{
    public class Cipher
    {
        CipherStrategy cipherStrategy;
        public Cipher(CipherStrategy cipherStrategy)
        {
            this.cipherStrategy = cipherStrategy;
        }
        public string Encrypt(string text)
        {
            return cipherStrategy.Encrypt(text);
        }
        public string Decrypt(string text)
        {
            return cipherStrategy.Decrypt(text);
        }
    }
}
