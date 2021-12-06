using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Num;

namespace FactApp
{
    class Program
    {
        static void Main(string[] args)
        {


           
            BigInteger i = 0;
            
            while (true)
            {
                Value.Factorial(i);
                i += 1000;
            }
            //Console.ReadLine();
            
        }
    }
}
