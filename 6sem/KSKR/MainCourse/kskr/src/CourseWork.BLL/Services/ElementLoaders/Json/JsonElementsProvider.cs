using CourseWork.Common.Models;
using CourseWork.BLL.Interfaces;
using CourseWork.BLL.Models;
using System.Collections.Generic;
using System.Linq;

namespace CourseWork.BLL.Services.ElementLoaders.Json
{
    /// <summary>
    /// Gets grid settings and creates elements by using them.
    /// </summary>
    public class JsonElementsProvider : IElementsProvider
    {
        private readonly string _settingsPath;
        private readonly IGridSerializer _gridSerializer;
        private readonly ILoadsProvider _loadsProvider;
        readonly double _step;

        public JsonElementsProvider(string settingsPath, IGridSerializer figureSerializer, ILoadsProvider loadsProvider = default, double step = 3)
        {
            _settingsPath = settingsPath;
            _gridSerializer = figureSerializer;
            _loadsProvider = loadsProvider;
            _step = step;
        }

        public FiniteElementModel GetModel()
        {
            var gridSettings = _gridSerializer.Deserialize(_settingsPath);
            var matrix = new int[gridSettings.Lenght, gridSettings.Width, gridSettings.Height];
            foreach (var figure in gridSettings.Figures)
            {
                figure.Draw(matrix, _step);
            }
            var (cubes, nodes) = GetCubes(matrix);
            for (var i = 0; i < nodes.Count; i++)
            {
                nodes[i].Id = i;
            }

            var elements = cubes.SelectMany(c => c.ToElements()).ToList();
            var nodeLoads = _loadsProvider != null ? _loadsProvider.GetNodeLoads() : new List<NodeLoad>();
            nodes.ForEach(n => n.Position = new Point(n.Position.X/100, n.Position.Y/100, n.Position.Z/100));
            return new FiniteElementModel(elements, nodes, nodeLoads);
        }

        /// <summary>
        /// Gets cubes for the specified matrix.
        /// </summary>
        /// <param name="matrix">Matrix filled by 0 and 1.</param>
        /// <returns>List of cubes.</returns>
        private (List<Cube>, List<Node> nodes) GetCubes(int[,,] matrix)
        {
            var cubes = new List<Cube>();
            var nodes = new List<Node>();
            for (var z = 0; z < matrix.GetLength(2)-1; z++)
            {
                for (var y = 0; y < matrix.GetLength(1)-1; y++)
                {
                    for (var x = 0; x < matrix.GetLength(0)-1; x++)
                    {
                        var nodePositions = GetNodePositions(x, y, z);
                        var cubeNodes = nodePositions.Select(p => matrix[(int)p.X, (int)p.Y, (int)p.Z]);
                        if (cubeNodes.All(n => n == 1))
                        {
                            cubes.Add(new Cube
                            {
                                Nodes = nodePositions.Select(p =>
                                {
                                    var existingNode = nodes.FirstOrDefault(n => n.Position.X == p.X && n.Position.Y == p.Y && n.Position.Z == p.Z);
                                    if(existingNode == null)
                                    {
                                        existingNode = new Node { Position = p };
                                        nodes.Add(existingNode);
                                    }

                                    return existingNode;
                                }).ToList()
                            });
                        };
                    }
                }
            }

            return (cubes, nodes);
        }

        /// <summary>
        /// Gets node positions for the Cube.
        /// </summary>
        /// <param name="x">X.</param>
        /// <param name="y">Y.</param>
        /// <param name="z">Z.</param>
        /// <returns>List of node positions.</returns>
        private List<Point> GetNodePositions(int x,int y,int z)
        {
            return new List<Point> 
            {
                new Point(x, y, z),
                new Point(x+1, y, z),
                new Point(x + 1, y, z+1),
                new Point(x, y, z+1),
                new Point(x, y+1, z),
                new Point(x+1, y+1, z),
                new Point(x + 1, y+1, z+1),
                new Point(x, y+1, z+1),
            };
        }
    }
}
