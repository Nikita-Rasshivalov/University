using System;
using System.Collections.Generic;
using System.Linq;

namespace KeyCipherLib
{
    public class PlayfairCipher : ICipher
    {
        string _key;
        public PlayfairCipher(string key)
        {
            _key = key;
        }

        public string Decode(string textToDecode)
        {
            return new CaesarForCheat().Decode(textToDecode, _key);
        }

        public string Encode(string textToEncode)
        {
            return new CaesarForCheat().Encode(textToEncode, _key);
        }
    }
}
