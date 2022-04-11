using System.Collections.Generic;

namespace FEM.DAL.Data
{
    /// <summary>
    /// Конечно-элементная сетка детали
    /// </summary>
    public class Mesh
    {
        /// <summary>
        /// Создание конечно-элементной сетки
        /// </summary>
        /// <param name="nodes">Узлы сетки</param>
        /// <param name="elements">Конечные элементы сетки</param>
        public Mesh(IList<Node> nodes, IList<Element> elements)
        {
            Nodes = nodes;
            Elements = elements;
        }
        /// <summary>
        /// Узлы сетки
        /// </summary>
        public IList<Node> Nodes { get; }
        /// <summary>
        /// Конечные элементы сетки
        /// </summary>
        public IList<Element> Elements { get; }
    }
}
