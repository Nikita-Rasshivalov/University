using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace StreamDecorators
{
    /// <summary>
    /// CalcStreamDecorator
    /// </summary>

    public class CalcStreamDecorator : StreamDecorator
    {
        /// <summary>
        /// Numbers of last bytes
        /// </summary>
        public int NumByte { get; set; }

        public List<byte> Values;
        /// <summary> 
        /// Creates an instance of CalcStreamDecorator class
        /// </summary>
        /// <param name="numByte">Numbers of last bytes</param>
        /// <param name="stream">stream</param>
        public CalcStreamDecorator(int numByte, Stream stream) : base(stream)
        {
            NumByte = numByte;
        }
        /// <summary>
        ///  Read an array of bytes
        /// </summary>
        /// <param name="buffer">buffer</param>
        /// <param name="offset">offset</param>
        /// <param name="count">count</param>
        /// <returns></returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            var col = stream.Read(buffer, offset, count);
            Values = new List<byte>();
            for (int i = col-NumByte; i < col; i++)
            {
                Values.Add(buffer[i]);
            }
            return col;
        }
    }
}
