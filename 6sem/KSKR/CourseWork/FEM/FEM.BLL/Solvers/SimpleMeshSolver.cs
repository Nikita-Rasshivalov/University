using Accord.Math;
using FEM.BLL.Data;
using FEM.BLL.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FEM.BLL.Solvers
{
    /// <summary>
    /// Класс, решающий задачи упругости методом конечных элементов
    /// </summary>
    public class SimpleMeshSolver : ISolver
    {
        /// <summary>
        /// Вычисляет перемещения узлов в сетке
        /// </summary>
        /// <param name="mesh">Конечно-элементная сетка, перемещения узлов которых вычисляются</param>
        /// <param name="material">Материал, из которого состоит деталь, на которую наложена эта сетка</param>
        /// <param name="nodeLoads">Узловые нагрузки конечно-элементной сетки</param>
        /// <returns></returns>
        public double[] GetNodesDisplacement(MeshDTO mesh, MaterialDTO material, IList<NodeLoadDTO> nodeLoads)
        {
            var globalStiffnessWithCondisplacements = GetGlobalStiffnessWithConstrains(mesh, material, nodeLoads);

            var nodeForces = GetNodeForces(mesh, nodeLoads);

            double[] displacement = new SLAESolver().Gauss(globalStiffnessWithCondisplacements, nodeForces);

            return displacement;
        }

        /// <summary>
        /// Метод, возвращающий перемещения конечных элементов после решения относительно исходной сетки
        /// </summary>
        /// <param name="originalMesh">Исходная сетка</param>
        /// <param name="nodesDisplacement">Перемещения всех узлов сетки</param>
        /// <returns>Вектор перемещений элементов</returns>
        public double[] GetElementsDisplacement(MeshDTO originalMesh, double[] nodesDisplacement)
        {
            IList<double> displacements = new List<double>();
            foreach (var element in originalMesh.Elements)
            {
                double xDisplacement = 0, yDisplacement = 0, zDisplacement = 0;
                foreach (var node in element.Nodes)
                {
                    xDisplacement += nodesDisplacement[node.Id * 3];
                    yDisplacement += nodesDisplacement[node.Id * 3 + 1];
                    zDisplacement += nodesDisplacement[node.Id * 3 + 2];
                }

                xDisplacement /= 4;
                yDisplacement /= 4;
                zDisplacement /= 4;

                displacements.Add(Math.Sqrt(Math.Pow(xDisplacement, 2) + Math.Pow(yDisplacement, 2) + Math.Pow(zDisplacement, 2)) / 7);
            }
            return displacements.ToArray();
        }

        /// <summary>
        /// Вычисляет деформации конечных элементов?
        /// </summary>
        /// <param name="originalMesh">Исходная сетка</param>
        /// <param name="nodesDisplacement">Перемещения всех узлов сетки</param>
        /// <returns></returns>
        public double[] GetElementsStrain(MeshDTO originalMesh, double[] nodesDisplacement)
        {
            IList<double> strain = new List<double>();

            foreach (var element in originalMesh.Elements)
            {
                IList<double> curElementNodeDisplacement = new List<double>();
                foreach (var node in element.Nodes)
                {
                    curElementNodeDisplacement.Add(nodesDisplacement[node.Id * 3]);
                    curElementNodeDisplacement.Add(nodesDisplacement[node.Id * 3 + 1]);
                    curElementNodeDisplacement.Add(nodesDisplacement[node.Id * 3 + 2]);
                }
                double[] currentDisplacement = curElementNodeDisplacement.ToArray();

                double[] currentStrain = element.GeometricMatrix.Dot(element.GetInversedA()).Dot(currentDisplacement);

                strain.Add(currentStrain[1] / 100);
            }
            return strain.ToArray();
        }
        /// <summary>
        /// Вычисляет напряжения конечных элементов
        /// </summary>
        /// <param name="originalMesh">Исходная сетка</param>
        /// <param name="nodesDisplacement">Перемещения всех узлов сетки</param>
        /// <param name="material">Материал детали</param>
        /// <returns></returns>
        public double[] GetElementsStresses(MeshDTO originalMesh, double[] nodesDisplacement, MaterialDTO material)
        {
            IList<double> stresses = new List<double>();

            foreach (var element in originalMesh.Elements)
            {
                IList<double> curElementNodeDisplacement = new List<double>();
                foreach (var node in element.Nodes)
                {
                    curElementNodeDisplacement.Add(nodesDisplacement[node.Id * 3]);
                    curElementNodeDisplacement.Add(nodesDisplacement[node.Id * 3 + 1]);
                    curElementNodeDisplacement.Add(nodesDisplacement[node.Id * 3 + 2]);
                }
                double[] currentDisplacement = curElementNodeDisplacement.ToArray();

                double[] currentStresses = element.GetElasticMatrix(material).Dot(element.GeometricMatrix).Dot(element.GetInversedA()).Dot(currentDisplacement);

                stresses.Add(currentStresses[1] / 500);
            }
            return stresses.ToArray();

        }

        /// <summary>
        /// Вычисляет вектор узловых усилий конечно-элементной сетки
        /// </summary>
        /// <param name="mesh">Конечно-элементная сетка, вектор узловых усилий которой необходимо вычислить</param>
        /// <param name="nodeLoads">Узловые нагрузки для сетки</param>
        /// <returns>Вектор узловых усилий конечно-элементной сетки</returns>
        private double[] GetNodeForces(MeshDTO mesh, IList<NodeLoadDTO> nodeLoads)
        {
            double[] forces = new double[mesh.Nodes.Count * 3];
            foreach (var load in nodeLoads)
            {
                if (load.NodeLoadType == NodeLoadType.Force)
                {
                    forces[load.Id * 3] = load.ForceVector.X;
                    forces[load.Id * 3 + 1] = load.ForceVector.Y;
                    forces[load.Id * 3 + 2] = load.ForceVector.Z;
                }
            }
            return forces;
        }

        /// <summary>
        /// Вычисляет матрицу жесткости детали с наложенными ограничениями в виде закреплений
        /// </summary>
        /// <param name="mesh">Конечно-элементная сетка детали</param>
        /// <param name="material">Материал детали</param>
        /// <param name="nodeLoads">Узловые нагрузки детали</param>
        /// <returns>Матрица жесткости детали с наложенными ограничениями в виде закреплений</returns>
        private double[,] GetGlobalStiffnessWithConstrains(MeshDTO mesh, MaterialDTO material, IList<NodeLoadDTO> nodeLoads)
        {
            var nodesCount = mesh.Nodes.Count;
            var globalStiffnessMatrix = mesh.GetStiffnessMatrix(material);
            foreach (var load in nodeLoads)
            {
                if (load.NodeLoadType == NodeLoadType.Fixed)
                {
                    for (int i = 0; i < nodesCount * 3; i++)
                    {
                        globalStiffnessMatrix[3 * load.Id, i] = 0;
                        globalStiffnessMatrix[3 * load.Id + 1, i] = 0;
                        globalStiffnessMatrix[3 * load.Id + 2, i] = 0;
                        globalStiffnessMatrix[i, 3 * load.Id] = 0;
                        globalStiffnessMatrix[i, 3 * load.Id + 1] = 0;
                        globalStiffnessMatrix[i, 3 * load.Id + 2] = 0;
                    }
                    globalStiffnessMatrix[3 * load.Id, 3 * load.Id] = 1;
                    globalStiffnessMatrix[3 * load.Id + 1, 3 * load.Id + 1] = 1;
                    globalStiffnessMatrix[3 * load.Id + 2, 3 * load.Id + 2] = 1;
                }
            }
            return globalStiffnessMatrix;
        }
    }
}
