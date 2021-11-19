using System;
using System.Collections.Generic;
using System.Text;

namespace KeyCipherLib
{
    public interface ICipher
    {
        public string Encode(string textToEncode);
        public string Decode(string textToDecode);
    }
}
