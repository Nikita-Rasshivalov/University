using System.Collections.Generic;

namespace FEM.BLL.Data
{
    /// <summary>
    /// Конечно-элементная сетка детали
    /// </summary>
    public class MeshDTO
    {
        /// <summary>
        /// Создание конечно-элементной сетки
        /// </summary>
        /// <param name="nodes">Узлы сетки</param>
        /// <param name="elements">Конечные элементы сетки</param>
        public MeshDTO(IList<NodeDTO> nodes, IList<ElementDTO> elements)
        {
            Nodes = nodes;
            Elements = elements;
        }
        /// <summary>
        /// Узлы сетки
        /// </summary>
        public IList<NodeDTO> Nodes { get; }
        /// <summary>
        /// Конечные элементы сетки
        /// </summary>
        public IList<ElementDTO> Elements { get; }
        /// <summary>
        /// Создаёт копию сетки
        /// </summary>
        /// <returns>Копия текущей сетки</returns>
        public MeshDTO Copy()
        {
            IList<NodeDTO> nodes = new List<NodeDTO>();
            IList<ElementDTO> elements = new List<ElementDTO>();

            foreach (var node in Nodes)
            {
                nodes.Add(node.Copy());
            }

            foreach (var element in Elements)
            {
                elements.Add(element.Copy(nodes));
            }

            return new MeshDTO(nodes, elements);
        }

        /// <summary>
        /// Получает глобальную матрицу жесткости сетки
        /// </summary>
        /// <param name="material">Материал детали сетки</param>
        /// <returns>Глобальная матрица жесткости</returns>
        public double[,] GetStiffnessMatrix(MaterialDTO material)
        {
            double[,] stiffnessMatrix = new double[Nodes.Count * 3, Nodes.Count * 3];
            foreach (var el in Elements)
            {
                int nodesCount = el.Nodes.Length;
                double[,] elStiffness = el.GetStiffnessMatrix(material);
                //Цикл для прохода по узлам матрицы жесткости конечного элемента
                for (int i = 0; i < nodesCount; i++)
                {
                    for (int j = 0; j < nodesCount; j++)
                    {
                        //Цикл для прохода по всем степеням свободы узла конечного элемента
                        for (int k = 0; k < 3; k++)
                        {
                            for (int m = 0; m < 3; m++)
                            {
                                stiffnessMatrix[3 * el.Nodes[i].Id + k, 3 * el.Nodes[j].Id + m] += elStiffness[3 * i + k, 3 * j + m];
                            }
                        }
                    }
                }
            }
            return stiffnessMatrix;
        }


    }
}
