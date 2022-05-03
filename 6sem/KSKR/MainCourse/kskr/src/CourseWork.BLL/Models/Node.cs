using CourseWork.Common.Models;
using System;

namespace CourseWork.BLL.Models
{
    public class Node : ICloneable
    {
        public int Id { get; set; }

        public Point Position { get; set; }

        public object Clone()
        {
            return new Node
            {
                Id = Id,
                Position = (Point)Position.Clone(),
            };
        }

        public override string ToString()
        {
            return Position.ToString();
        }
    }
}
