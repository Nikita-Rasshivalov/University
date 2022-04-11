using FEM.BLL.Data;
using System.Collections.Generic;

namespace FEM.BLL.Solvers
{
    public interface ISolver
    {
        /// <summary>
        /// Метод, вычисляющий узловые перемещения сетки
        /// </summary>
        /// <param name="mesh">Конечно-элементная сетка</param>
        /// <param name="material">Материал сетки</param>
        /// <param name="nodeLoads">Узловые нагрузки</param>
        /// <returns>Узловые перемещения сетки</returns>
        public double[] GetNodesDisplacement(MeshDTO mesh, MaterialDTO material, IList<NodeLoadDTO> nodeLoads);

        /// <summary>
        /// Вычисляет перемещения конечных элементов после решения относительно исходной сетки
        /// </summary>
        /// <param name="originalMesh">Исходная сетка</param>
        /// <param name="solutionMesh">Сетка после решения</param>
        /// <returns>Вектор перемещений элементов</returns>
        public double[] GetElementsDisplacement(MeshDTO originalMesh, double[] nodesDisplacement);
        /// <summary>
        /// Вычисляет деформации конечных элементов
        /// </summary>
        /// <param name="originalMesh">Исходная сетка</param>
        /// <param name="nodesDisplacement">Перемещения всех узлов сетки</param>
        /// <returns></returns>
        public double[] GetElementsStrain(MeshDTO originalMesh, double[] nodesDisplacement);
        /// <summary>
        /// Вычисляет напряжения конечных элементов
        /// </summary>
        /// <param name="originalMesh">Исходная сетка</param>
        /// <param name="nodesDisplacement">Перемещения всех узлов сетки</param>
        /// <param name="material">Материал детали</param>
        /// <returns></returns>
        public abstract double[] GetElementsStresses(MeshDTO originalMesh, double[] nodesDisplacement, MaterialDTO material);
    }
}
