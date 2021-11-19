using System;
using System.Collections.Generic;
using System.Text;

namespace KeyCipherLib
{
    class CaesarForCheat
    {
        public string Encode(string text, string keyText)
        {
            int key = keyText.Length;
            StringBuilder builder = new StringBuilder(text.Length);
            foreach(char c in text)
            {
                builder.Append((char)(c + key));
            }
            return builder.ToString();
        }
        public string Decode(string text, string keyText)
        {
            try
            {
                int key = keyText.Length;
                StringBuilder builder = new StringBuilder(text.Length);
                foreach (char c in text)
                {
                    builder.Append((char)(c - key));
                }
                return builder.ToString();
            }
            catch
            {
                throw new Exception("Неправильный ключ");
            }
        }
    }
}
