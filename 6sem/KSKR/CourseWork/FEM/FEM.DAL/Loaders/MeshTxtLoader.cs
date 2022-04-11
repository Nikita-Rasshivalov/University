using FEM.DAL.Data;
using FEM.DAL.Data.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FEM.DAL.Loaders
{
    /// <summary>
    /// Класс для загрузки сетки из txt файла
    /// </summary>
    public class MeshTxtLoader : ILoader<Mesh>
    {
        readonly string[] _elementTypes;
        readonly string _pathToNodes;
        readonly string _pathToElements;
        /// <summary>
        /// Создание загрузчика сетки
        /// </summary>
        /// <param name="pathToNodes">Путь к файлу с узлами</param>
        /// <param name="pathToElements">Путь к файлу с элементами</param>
        public MeshTxtLoader(string pathToNodes, string pathToElements)
        {
            _pathToNodes = pathToNodes;
            _pathToElements = pathToElements;
            _elementTypes = new string[]
            {
                "TETRA_4"
            };
        }
        /// <summary>
        /// Загрузка сетки из txt файла
        /// </summary>
        /// <returns>Конечно-элементная сетка</returns>
        public Mesh Load()
        {
            try
            {
                IList<Node> nodes = GetNodes();
                IList<Element> elements = GetElements();
                return new Mesh(nodes, elements);
            }
            catch (Exception ex)
            {
                throw new Exception($"Не удалось загрузить сетку {ex.Message}");
            }
        }

        /// <summary>
        /// Получает узлы конечно-элементной сетки из файла с узлами
        /// </summary>
        /// <returns>Узлы конечно-элементной сетки</returns>
        private IList<Node> GetNodes()
        {
            IList<Node> nodes = new List<Node>();
            using (StreamReader sr = new StreamReader(_pathToNodes))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string rowType = line.Split(' ')[0];
                    if (rowType == "node")
                    {
                        nodes.Add(GetNodeFromLine(line));
                    }
                }
            }
            return nodes;
        }

        /// <summary>
        /// Получает элементы конечно-элементной сетки из файла с узлами
        /// </summary>
        /// <returns>Элементы конечно-элементной сетки</returns>
        public IList<Element> GetElements()
        {
            IList<Element> elements = new List<Element>();
            using (StreamReader sr = new StreamReader(_pathToElements))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string rowType = line.Split(' ')[0];
                    if (_elementTypes.Contains(rowType))
                    {
                        elements.Add(GetElementFromLine(line));
                    }
                }
            }
            return elements;
        }

        /// <summary>
        /// Преобразует строку из файла с узлами в класс, содержащий информацию об узле
        /// </summary>
        /// <param name="line">Строка из файла с узлами</param>
        /// <returns>Класс узла конечно-элементной сетки</returns>
        private Node GetNodeFromLine(string line)
        {
            Regex regex = new Regex(@"internal\s(?<Id>\d+).*\s(?<X>-?\d+(\.\d+)?(e-\d+)?)\s(?<Y>-?\d+(\.\d+)?(e-\d+)?)\s(?<Z>-?\d+(\.\d+)?(e-\d+)?)");
            Match match = regex.Match(line);
            int id = int.Parse(match.Groups["Id"].Value);
            double x = double.Parse(match.Groups["X"].Value.Replace(".", ","));
            double y = double.Parse(match.Groups["Y"].Value.Replace(".", ","));
            double z = double.Parse(match.Groups["Z"].Value.Replace(".", ","));
            return new Node(id, x, y, z);
        }

        /// <summary>
        /// Преобразует строку из файла с элементами в класс, содержащий информацию об элементе, в зависимости от вида
        /// конечного элемента
        /// </summary>
        /// <param name="line">Строка из файла с элементами</param>
        /// <returns>Класс элемента конечно-элементной сетки</returns>
        private Element GetElementFromLine(string line)
        {
            string elementType = line.Split(' ')[0];
            Regex regex;
            int id;
            int[] nodeIds;
            switch (elementType)
            {
                case "TETRA_4":
                    regex = new Regex(@"internal\s(?<Id>\d+).*\s(?<Node1>\d+)\s(?<Node2>\d+)\s(?<Node3>\d+)\s(?<Node4>\d+)");
                    Match match = regex.Match(line);
                    id = int.Parse(match.Groups["Id"].Value);
                    nodeIds = new int[]
                    {
                        int.Parse(match.Groups["Node1"].Value),
                        int.Parse(match.Groups["Node2"].Value),
                        int.Parse(match.Groups["Node3"].Value),
                        int.Parse(match.Groups["Node4"].Value)
                    };
                    return new LinearTetrahedron(id, nodeIds);
                default:
                    throw new Exception("Нет такого типа конечного элемента");
            }
        }
    }
}
