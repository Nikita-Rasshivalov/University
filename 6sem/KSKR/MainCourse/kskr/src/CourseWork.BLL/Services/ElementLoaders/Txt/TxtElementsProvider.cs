using CourseWork.Common.Models;
using CourseWork.BLL.Interfaces;
using CourseWork.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseWork.BLL.Services.ElementLoaders.Txt
{
    /// <summary>
    /// Gets elements from txt(ANSYS format) file.
    /// </summary>
    public class TxtElementsProvider : BaseTxtProvider, IElementsProvider
    {
        private const string Separator = "\t";
        private readonly string _elementsPath;
        private readonly string _nodesPath;
        private readonly ILoadsProvider _loadsProvider;

        public TxtElementsProvider(string elementsPath, string nodesPath, ILoadsProvider loadsProvider = default)
        {
            _elementsPath = elementsPath;
            _nodesPath = nodesPath;
            _loadsProvider = loadsProvider;
        }

        public FiniteElementModel GetModel()
        {
            var elements = new List<Element>();
            var nodes = GetNodes();
            var nodeLoads = _loadsProvider != null ? _loadsProvider.GetNodeLoads() : new List<NodeLoad>();
            var elementRows = GetValuesFromTextFile(_elementsPath, Separator, 1).ToList();
            if (elementRows.Any())
            {
                if (elementRows[0].Length < 3)
                {
                    throw new InvalidOperationException($"Element row ''{string.Join(" ", elementRows[0])}' has no node ids.");
                }

                if (Enum.TryParse<ElementType>(elementRows[0][1], true, out var elementType))
                {
                    var rowValuesCount = 2 + elementType.GetNodeCount();
                    if (elementRows.Any(r => r.Length != rowValuesCount))
                    {
                        throw new InvalidOperationException($"Each element row of type '{elementType}' should have '{rowValuesCount}' values.");
                    }
                    foreach (var row in elementRows)
                    {
                        if (!int.TryParse(row[0], out var id))
                        {
                            throw new InvalidOperationException($"Couldn't parse element id '{row[0]}'.");
                        }
                        var elementNodes = GetElementNodes(row.Skip(2), nodes).ToList();
                        elements.Add(new Element
                        {
                            Id = id-1,
                            ElementType = elementType,
                            Nodes = elementNodes,
                        });
                    }
                }
                else
                {
                    throw new InvalidOperationException($"Found unknown element type {elementRows[0][1]}.");
                }
            }

            return new FiniteElementModel(elements, nodes, nodeLoads);
        }

        /// <summary>
        /// Loads all nodes from path.
        /// </summary>
        /// <returns>Nodes list.</returns>
        private List<Node> GetNodes()
        {
            var nodes = new List<Node>();
            var rows = GetValuesFromTextFile(_nodesPath, Separator, 1);
            foreach (var row in rows)
            {
                if (row.Length != 4)
                {
                    throw new InvalidOperationException($"Invalid format of node '{string.Join(" ",row)}");
                }
                if (int.TryParse(row[0], out var id)
                    && double.TryParse(row[1], out var x)
                    && double.TryParse(row[2], out var y)
                    && double.TryParse(row[3], out var z))
                {
                    nodes.Add(new Node
                    {
                        Id = id-1,
                        Position = new Point(x, y, z),
                    });
                }
                else
                {
                    throw new InvalidOperationException($"Couldn't parse node values '{string.Join(" ", row)}'.");
                }
            }

            return nodes;
        }

        /// <summary>
        /// Gets nodes for element by string ids.
        /// </summary>
        /// <param name="nodeIds">List of node ids.</param>
        /// <param name="nodes">All nodes.</param>
        /// <returns>Element nodes.</returns>
        private List<Node> GetElementNodes(IEnumerable<string> nodeIds, List<Node> nodes)
        {
            return nodeIds.Select(stringNodeId =>
                   {
                       if (!int.TryParse(stringNodeId, out var nodeId))
                       {
                           throw new InvalidOperationException($"Couldn't parse node id '{stringNodeId}'.");
                       }

                       return nodeId;
                   })
                   .Select(nodeId =>
                   {
                       var node = nodes.FirstOrDefault(n => n.Id == nodeId-1);
                       if (node is null)
                       {
                           throw new InvalidOperationException($"Node with id '{nodeId}' was not found.");
                       }

                       return node;
                   }).ToList();
        }
    }
}
