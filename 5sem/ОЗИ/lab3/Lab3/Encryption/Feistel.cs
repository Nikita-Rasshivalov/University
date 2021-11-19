using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    public static class Feistel
    {
        public static string Encode(string message, string key)
        {
            return Code(message, key);
        }

        public static string Dencode(string message, string key)
        {
            return Code(message, key);
        }

        private static string Code(string message, string key)
        {
            byte[] messageBytes = Encoding.Default.GetBytes(message);
            byte[] keyBytes = Encoding.Default.GetBytes(key);

            int diff = messageBytes.Length % 8;
            if (diff != 0)
            {
                byte[] temp = new byte[messageBytes.Length + (8 - diff)];
                Array.Copy(messageBytes, temp, messageBytes.Length);
                messageBytes = temp;
            }

            byte[] result= new byte[messageBytes.Length];

            for (int i = 0; i < messageBytes.Length; i = i + 8)
            {
                byte[] block = new byte[8];
                Array.Copy(messageBytes, i, block, 0, 8);

                for (int j = 0; j < 16; j++)
                {
                    byte[] leftBlock = new byte[4];
                    Array.Copy(block, leftBlock, 4);
                    byte[] rightBlock = new byte[4];
                    Array.Copy(block, 4, rightBlock, 0, 4);

                    byte[] keyPart = new byte[4];
                    Array.Copy(keyBytes, keyPart, 4);
                    keyPart = keyGen(keyBytes, j);

                    Pbox(block);
                    block = CodeBlock(leftBlock, rightBlock, keyPart, true);
                }

                for (int j = 15; j >= 0; j--)
                {
                    byte[] leftBlock = new byte[4];
                    Array.Copy(block, leftBlock, 4);
                    byte[] rightBlock = new byte[4];
                    Array.Copy(block, 4, rightBlock, 0, 4);

                    byte[] keyPart = new byte[4];
                    Array.Copy(keyBytes, keyPart, 4);
                    keyPart = keyGen(keyBytes, j);

                    if (j != 0)
                    {
                        Pbox(block);
                        block = CodeBlock(leftBlock, rightBlock, keyPart, false);
                    }
                    else
                        block = CodeBlock(leftBlock, rightBlock, keyPart, true);
                }
                Array.Copy(block, 0, result, i, block.Length);
            }
            return Encoding.Default.GetString(result);
        }

        private static byte[] Pbox(byte[] block)
        {
            byte tmp = block[block.Length - 1];
            for (int i = block.Length - 1; i > 0; i--)
            {
                block[i] = block[i - 1];
            }
            block[0] = tmp;
            return block;
        }

        private static byte[] CodeBlock(byte[] leftBlock, byte[] rightBlock, byte[] key, bool isLast)
        {
            int intLeftBlock = BitConverter.ToInt32(leftBlock, 0);
            int intKey = BitConverter.ToInt32(key, 0);

            intLeftBlock = intLeftBlock ^ intKey;
            leftBlock = BitConverter.GetBytes(intLeftBlock);

            byte[] result = new byte[8];
            if (!isLast)
            {
                Array.Copy(rightBlock, result, 4);
                Array.Copy(leftBlock, 0, result, 4, 4);
            }
            else
            {
                Array.Copy(leftBlock, result, 4);
                Array.Copy(rightBlock, 0, result, 4, 4);
            }
            return result;
        }

        private static byte[] keyGen(byte[] key, int i)
        {
            byte[] tmp = new byte[4];
            Array.Copy(key, tmp, 4);
            int left = BitConverter.ToInt32(tmp, 0);
            Array.Copy(key, 4, tmp, 0, 4);
            int right = BitConverter.ToInt32(tmp, 0);
 
            int l_l = left << (i * 3);
            int r_r = right >> (32 - i * 3);
            left = l_l + r_r;
 
            return BitConverter.GetBytes(left);
        }
    }
}
