using CourseWork.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseWork.BLL.Models
{
    public class FiniteElementModel : ICloneable
    {
        public FiniteElementModel(List<Element> elements, List<Node> nodes, List<NodeLoad> nodeLoads = default)
        {
            Elements = elements;
            Nodes = nodes;
            if (nodeLoads != null)
            {
                NodeLoads = nodeLoads;
            }
        }

        public List<Element> Elements { get; set; } = new List<Element>();

        public List<Node> Nodes { get; set; } = new List<Node>();

        public List<NodeLoad> NodeLoads { get; set; } = new List<NodeLoad>();

        /// <summary>
        /// Gets global stiffness matrix.
        /// </summary>
        /// <param name="material">Material.</param>
        /// <returns>Global stiffness matrix.</returns>
        public double[,] GetGlobalStiffnessMatrix(DetailMaterial material)
        {
            var globalMatrix = new double[Nodes.Count * 3, Nodes.Count * 3];
            foreach (var element in Elements)
            {
                var localMatrix = element.GetLocalStiffnessMatrix(material);
                for (int i = 0; i < element.Nodes.Count; i++)
                {
                    for (int j = 0; j < element.Nodes.Count; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            for (int n = 0; n < 3; n++)
                            {
                                globalMatrix[3 * element.Nodes[i].Id + k, 3 * element.Nodes[j].Id + n] += localMatrix[3 * i + k, 3 * j + n];
                            }
                        }
                    }
                }
            }

            return ApplyFixedSupport(globalMatrix);
        }

        public double[] GetNodeForces()
        {
            double[] forces = new double[Nodes.Count * 3];
            foreach (var load in NodeLoads)
            {
                if (load.LoadType == LoadType.Force)
                {
                    forces[load.NodeId * 3] = load.X;
                    forces[load.NodeId * 3 + 1] = load.Y;
                    forces[load.NodeId * 3 + 2] = load.Z;
                }
            }

            return forces;
        }

        private double[,] ApplyFixedSupport(double[,] globalMatrix)
        {
            foreach (var load in NodeLoads)
            {
                if (load.LoadType == LoadType.Fixed)
                {
                    for (int i = 0; i < Nodes.Count * 3; i++)
                    {
                        globalMatrix[3 * load.NodeId, i] = 0;
                        globalMatrix[3 * load.NodeId + 1, i] = 0;
                        globalMatrix[3 * load.NodeId + 2, i] = 0;
                        globalMatrix[i, 3 * load.NodeId] = 0;
                        globalMatrix[i, 3 * load.NodeId + 1] = 0;
                        globalMatrix[i, 3 * load.NodeId + 2] = 0;
                    }
                    globalMatrix[3 * load.NodeId, 3 * load.NodeId] = 1;
                    globalMatrix[3 * load.NodeId + 1, 3 * load.NodeId + 1] = 1;
                    globalMatrix[3 * load.NodeId + 2, 3 * load.NodeId + 2] = 1;
                }
            }

            return globalMatrix;
        }

        public object Clone()
        {
            var newNodes = new List<Node>();
            foreach(var node in Nodes)
            {
                newNodes.Add(new Node { Id = node.Id, Position = new Point(node.Position.X, node.Position.Y, node.Position.Z) });
            }
            var newElements = new List<Element>();
            foreach(var element in Elements)
            {
                newElements.Add(new Element { ElementType = element.ElementType, Id = element.Id, Nodes = newNodes.Where(n => (new int[] { element.Nodes[0].Id, element.Nodes[1].Id, element.Nodes[2].Id, element.Nodes[3].Id }).Any(id => id == n.Id)).ToList() });
            }

            return new FiniteElementModel(newElements, newNodes, NodeLoads);
        }
    }
}
