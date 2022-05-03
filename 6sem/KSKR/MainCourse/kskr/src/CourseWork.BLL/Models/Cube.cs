using System.Collections.Generic;

namespace CourseWork.BLL.Models
{
    public class Cube
    {
        public List<Node> Nodes { get; set; }

        public List<Element> ToElements()
        {
            return new List<Element> {
            new Element
            {
                Id = 1,
                Nodes = new List<Node> { Nodes[0], Nodes[1], Nodes[4], Nodes[7] }
            },
            new Element
            {
                Id = 2,
                Nodes = new List<Node> { Nodes[1], Nodes[4], Nodes[5], Nodes[7] }
            },
            new Element
            {
                Id = 3,
                Nodes = new List<Node> { Nodes[0], Nodes[1], Nodes[3], Nodes[7] }
            },
            new Element
            {
                Id = 4,
                Nodes = new List<Node> { Nodes[1], Nodes[5], Nodes[6], Nodes[7] }
            },
            new Element
            {
                Id = 5,
                Nodes = new List<Node> { Nodes[1], Nodes[2], Nodes[6], Nodes[7] }
            },
            new Element
            {
                Id = 6,
                Nodes = new List<Node> { Nodes[1], Nodes[2], Nodes[3], Nodes[7] }
            }
            };
        }
    }
}
