using FEM.BLL.Data;
using FEM.BLL.Data.Elements;
using FEM.BLL.MeshGenerators.Figures;
using System.Collections.Generic;

namespace FEM.BLL.Generators
{
    /// <summary>
    /// Класс, генерирующий различные сетки
    /// </summary>
    public class MeshGenerator
    {
        /// <summary>
        /// Получает копию текущей сетки, в которой узлы перемещены
        /// </summary>
        /// <param name="nodesDisplacement">Вектор узловых перемещений</param>
        /// <returns>Копия текущей сетки, в которой узлы перемещены</returns>
        public MeshDTO GenerateMovedMesh(MeshDTO mesh, double[] nodesDisplacement)
        {
            MeshDTO copy = mesh.Copy();
            foreach (var node in copy.Nodes)
            {
                node.Move(nodesDisplacement[node.Id * 3], nodesDisplacement[node.Id * 3 + 1], nodesDisplacement[node.Id * 3 + 2]);
            }
            return copy;
        }

        /// <summary>
        /// Генерация сетки для цилиндра
        /// </summary>
        /// <param name="radius">Радиус цилиндра</param>
        /// <param name="height">Высота цилиндра</param>
        /// <returns>Сетка для цилиндра</returns>
        public MeshDTO GetCylinderMesh(double radius, double height)
        {
            Prism cylinder = new Prism(radius, height);
            (IList<TrianglePrism> prisms, IList<NodeDTO> nodes) = cylinder.GetTrianglePrismsWithNodes();
            IList<LinearTetrahedronDTO> tetrahedrons = new List<LinearTetrahedronDTO>();
            foreach (var prism in prisms)
            {
                foreach (var tetra in prism.GetLinearTetrahedrons())
                {
                    tetrahedrons.Add(tetra);
                }
            }
            return new MeshDTO(nodes, new List<ElementDTO>(tetrahedrons));
        }

        /// <summary>
        /// Генерация сетки для многоугольной призмы
        /// </summary>
        /// <param name="sidesCount">Количество сторон призмы</param>
        /// <param name="sidesLength">Длина стороны призмы</param>
        /// <param name="height">Высота призмы</param>
        /// <returns></returns>
        public MeshDTO GetPolygonMesh(int sidesCount, double sidesLength, double height)
        {
            Prism poli = new Prism(sidesCount, sidesLength, height);
            (IList<TrianglePrism> prisms, IList<NodeDTO> nodes) = poli.GetTrianglePrismsWithNodes();
            IList<LinearTetrahedronDTO> tetrahedrons = new List<LinearTetrahedronDTO>();
            foreach (var prism in prisms)
            {
                foreach (var tetra in prism.GetLinearTetrahedrons())
                {
                    tetrahedrons.Add(tetra);
                }
            }
            return new MeshDTO(nodes, new List<ElementDTO>(tetrahedrons));
        }
    }
}
