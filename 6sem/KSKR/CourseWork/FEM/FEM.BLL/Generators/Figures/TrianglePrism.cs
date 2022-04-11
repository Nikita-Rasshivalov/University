using FEM.BLL.Data;
using FEM.BLL.Data.Elements;
using System.Collections.Generic;
using System.Drawing;

namespace FEM.BLL.MeshGenerators.Figures
{
    /// <summary>
    /// Класс треугольной призмы
    /// </summary>
    public class TrianglePrism
    {
        int _id;
        NodeDTO[] _nodes;
        /// <summary>
        /// Создание класса треугольной призмы
        /// </summary>
        /// <param name="id">Идентификатор призмы</param>
        /// <param name="nodes">Узлы треугольной призмы</param>
        public TrianglePrism(int id, NodeDTO[] nodes)
        {
            _id = id;
            _nodes = nodes;
        }

        /// <summary>
        /// Разбиение призмы на линейные тетраэдры
        /// </summary>
        /// <returns>Список линейных тетраэдров из которых состоит призма</returns>
        public IList<LinearTetrahedronDTO> GetLinearTetrahedrons()
        {
            IList<LinearTetrahedronDTO> tetras = new List<LinearTetrahedronDTO>();
            tetras.Add(new LinearTetrahedronDTO(_id * 3, new NodeDTO[] { _nodes[0], _nodes[2], _nodes[3], _nodes[4] }, Color.Gray));
            tetras.Add(new LinearTetrahedronDTO(_id * 3, new NodeDTO[] { _nodes[1], _nodes[4], _nodes[3], _nodes[5] }, Color.Gray));
            tetras.Add(new LinearTetrahedronDTO(_id * 3, new NodeDTO[] { _nodes[0], _nodes[4], _nodes[3], _nodes[1] }, Color.Gray));
            return tetras;
        }
    }
}
