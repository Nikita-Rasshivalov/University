using System;

namespace CourseWork.BLL.Models
{
    public class NodeLoad : ICloneable
    {
        public LoadType LoadType { get; set; }

        public int NodeId { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public object Clone()
        {
            return new NodeLoad
            {
                LoadType = LoadType,
                NodeId = NodeId,
                X = X,
                Y = Y,
                Z = Z,
            };
        }
    }
}
