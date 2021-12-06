using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace Num
{
    public class Value
    {
        public static BigInteger Factorial( BigInteger x)
        {
            if (x == 0)
            {
                return 1;
            }
            else
            {                
                    return x * Factorial(x - 1);  
            }
        }

        public static BigInteger ChechValue(BigInteger x)
        {
            BigInteger value = Factorial(x);
            while (x != 0)
            {
                value /= x;
                x--;
            }
            return value;
        }
    }
}
