using System;
using System.Collections.Generic;
using System.Text;

namespace KeyCipherLib
{
    public class CardanoGrid : ICipher
    {
        static string Encode(string word)
        {
            var sb = new StringBuilder();
            int i = 0, j = 0;

            for (; i < word.Length; i++)
            {
                for (; j < input.Length; j++)
                {
                    if (word[i] == input[j])
                    {
                        sb.Append("1");
                        j++;
                        break;
                    }

                    sb.Append("0");
                }
            }
            for (; j < input.Length; j++)
                sb.Append("0");

            return sb.ToString();
        }

        public static void Decode(string word)
        {
            // прямой обход решетки
            for (int i = 0; i < word.Length; i++)
            {
                if (Convert.ToString(word[i]) == "1")
                {
                    Console.Write(word[i]);
                }
            }
            Console.WriteLine("");
        }
    }
}
