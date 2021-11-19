using System.Collections.Generic;
using System.Text;

namespace СipherLibrary
{
    /// <summary>
    /// Класс реализующий цифровую систему тайнопими
    /// </summary>
    public class DigitalCipherStrategy : CipherStrategy
    {
        Dictionary<char, int> tokens;
        Dictionary<int, char> inversedTokens;

        public DigitalCipherStrategy()
        {
            tokens = new Dictionary<char, int>();
            inversedTokens = new Dictionary<int, char>();

            for (int i = 41; i < 100; i++) // Добавляю в словарь символы и их числовое представление
            {
                tokens.Add((char)(i - 9), i);
                inversedTokens.Add(i, (char)(i - 9));
            }
            for(int i = 10; i < 37; i++)
            {
                tokens.Add((char)(i + 87), i);
                inversedTokens.Add(i, (char)(i + 87));
            }
        }
        /// <summary>
        /// Метод дешифрования
        /// </summary>
        /// <param name="textToDecrypt">зашифрованный текст</param>
        /// <returns></returns>
        public string Decrypt(string textToDecrypt)
        {
            StringBuilder builder = new StringBuilder(textToDecrypt.Length);
            for(int i = 0; i < textToDecrypt.Length; i += 2)
            {
                if(i == textToDecrypt.Length - 1)
                {
                    builder.Append(i.ToString());
                }
                try
                {
                    int num = int.Parse(textToDecrypt.Substring(i, 2));
                    builder.Append(inversedTokens[num]);
                }
                catch
                {
                    builder.Append(textToDecrypt[i]);
                    i -= 1;
                }
            }
            return builder.ToString();
        }
        /// <summary>
        /// Метод шифрования
        /// </summary>
        /// <param name="textToEncrypt"> исходный текст</param>
        /// <returns></returns>
        public string Encrypt(string textToEncrypt)
        {
            StringBuilder builder = new StringBuilder(textToEncrypt.Length * 2);
            foreach (char c in textToEncrypt)
            {
                try
                {
                    builder.Append(tokens[c].ToString());
                }
                catch
                {
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }
    }
}
