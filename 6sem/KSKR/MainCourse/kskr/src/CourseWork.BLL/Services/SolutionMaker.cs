using Accord.Math;
using CourseWork.BLL.Interfaces;
using CourseWork.BLL.Models;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.Solvers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseWork.BLL.Services
{
    delegate int calc(double v1, double v2);
    public class SolutionMaker : ISolutionMaker
    {
        public double[] GetNodeDisplacements(FiniteElementModel model, DetailMaterial material)
        {
            var globalStiffnessWithCondisplacements = model.GetGlobalStiffnessMatrix(material);

            var nodeForces = model.GetNodeForces();
            var displacement = new SLAESolver().Gauss(globalStiffnessWithCondisplacements, nodeForces);

            return displacement;
        }

        public double[] GetElementsDisplacement(FiniteElementModel model, double[] nodeDisplacements, double coefficient = 1000)
        {
            var displacements = new List<double>();
            foreach (var element in model.Elements)
            {
                double xDisplacement = 0, yDisplacement = 0, zDisplacement = 0;
                foreach (var node in element.Nodes)
                {
                    xDisplacement += nodeDisplacements[node.Id * 3];
                    yDisplacement += nodeDisplacements[node.Id * 3 + 1];
                    zDisplacement += nodeDisplacements[node.Id * 3 + 2];
                }
                xDisplacement /= 4;
                yDisplacement /= 4;
                zDisplacement /= 4;

                displacements.Add(Math.Sqrt(Math.Pow(xDisplacement, 2) + Math.Pow(yDisplacement, 2) + Math.Pow(zDisplacement, 2)));
            }
            foreach(var node in model.Nodes)
            {
                node.Position.X += nodeDisplacements[node.Id * 3] * coefficient;
                node.Position.Y += nodeDisplacements[node.Id * 3 + 1] * coefficient;
                node.Position.Z += nodeDisplacements[node.Id * 3 + 2] * coefficient;
            }
            return displacements.ToArray();
        }


        /// <summary>
        /// Вычисляет деформации конечных элементов?
        /// </summary>
        /// <param name="originalMesh">Исходная сетка</param>
        /// <param name="nodesDisplacement">Перемещения всех узлов сетки</param>
        /// <returns></returns>
        public double[] GetElementsStrain(FiniteElementModel originalMesh, double[] nodesDisplacement)
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

                double[] elementStrains = element.GeometricMatrix.Dot(currentDisplacement);

                strain.Add(Math.Sqrt(2) / 3 * Math.Sqrt(Math.Pow(elementStrains[0] - elementStrains[1], 2) + Math.Pow(elementStrains[1] - elementStrains[2], 2) + Math.Pow(elementStrains[0] - elementStrains[2], 2) + 3/2 * (Math.Pow(elementStrains[3], 2) + Math.Pow(elementStrains[4], 2) + Math.Pow(elementStrains[5], 2))));
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
        public double[] GetElementsStresses(FiniteElementModel originalMesh, double[] nodesDisplacement, DetailMaterial material)
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

                double[] elementStresses = element.getElastic(material).Dot(element.GeometricMatrix.Dot(currentDisplacement));

                stresses.Add(1/Math.Sqrt(2) * Math.Sqrt(Math.Pow(elementStresses[0] - elementStresses[1],2) + Math.Pow(elementStresses[1] - elementStresses[2], 2) + Math.Pow(elementStresses[0] - elementStresses[2], 2) + 6* (Math.Pow(elementStresses[3],2) + Math.Pow(elementStresses[4], 2) + Math.Pow(elementStresses[5], 2))));
            }
            return stresses.ToArray();
        }
    }
}
