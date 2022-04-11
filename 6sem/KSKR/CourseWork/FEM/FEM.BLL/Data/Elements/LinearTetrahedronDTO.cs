using Accord.Math;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Media.Media3D;

namespace FEM.BLL.Data.Elements
{
    /// <summary>
    /// Линейный тетраэдральный конечный элемент
    /// </summary>
    public class LinearTetrahedronDTO : ElementDTO
    {
        /// <summary>
        /// Создание линейного тетраэдрального конечного элемента
        /// </summary>
        /// <param name="id">Идентификатор конечного элемента</param>
        /// <param name="nodes">Узлы конечного элемента</param>
        /// <param name="color">Цвет конечного элемента</param>
        public LinearTetrahedronDTO(int id, NodeDTO[] nodes, Color color) : base(id, nodes, color)
        {
            if (nodes.Length != 4)
            {
                throw new Exception("У линейного тетраэдра должно быть 4 различных узла");
            }
        }

        /// <summary>
        /// Объём конечного элемента
        /// </summary>
        public override double Volume
        {
            get
            {
                return Math.Abs(Matrix.Determinant(CoordinateMatrix) / 6);
            }
        }

        /// <summary>
        /// Вычисление матрицы жесткости линейного тетраэдра
        /// </summary>
        /// <param name="material">Материал тетраэдра</param>
        /// <returns>Матрица жесткости линейного тетраэдра</returns>
        public override double[,] GetStiffnessMatrix(MaterialDTO material)
        {
            return GeometricMatrix.Transpose().Dot(GetElasticMatrix(material)).Dot(GeometricMatrix).Multiply(Volume);
        }

        /// <summary>
        /// Координатная матрица конечного элемента
        /// </summary>
        private double[,] CoordinateMatrix
        {
            get
            {
                return new double[,]
                {
                    {1, Nodes[0].X, Nodes[0].Y, Nodes[0].Z },
                    {1, Nodes[1].X, Nodes[1].Y, Nodes[1].Z },
                    {1, Nodes[2].X, Nodes[2].Y, Nodes[2].Z },
                    {1, Nodes[3].X, Nodes[3].Y, Nodes[3].Z }
                };
            }
        }

        /// <summary>
        /// Геометрическая матрица конечного элемента [D]
        /// </summary>
        public override double[,] GeometricMatrix
        {
            get
            {
                double[,] Q = new double[6, 12];
                Q[0, 1] = 1;
                Q[1, 6] = 1;
                Q[2, 11] = 1;
                Q[3, 2] = 1;
                Q[3, 5] = 1;
                Q[4, 7] = 1;
                Q[4, 10] = 1;
                Q[5, 3] = 1;
                Q[5, 9] = 1;

                return Q.Dot(GetInversedA());
            }
        }

        public override Point3D CenterPoint
        {
            get
            {
                Point3D center = new Point3D(0, 0, 0);
                foreach (var node in Nodes)
                {
                    center.Offset(node.X, node.Y, node.Z);
                }
                double coef = 1.0 / (Nodes.Length + 1);
                center.X *= coef;
                center.Y *= coef;
                center.Z *= coef;
                return center;
            }
        }

        /// <summary>
        /// Вычисляет обратную матрицу А для вычисления геометрической матрицы
        /// </summary>
        /// <returns>Обратная матрица А</returns>
        public override double[,] GetInversedA()
        {
            double[,] inversedA = new double[12, 12];
            for (int i = 0; i < 4; i++)
            {
                (double A, double B, double C, double D) = GetABCD(i);
                for (int j = 0; j < 3; j++)
                {
                    inversedA[4 * j, i * 3 + j] = A;
                    inversedA[4 * j + 1, i * 3 + j] = B;
                    inversedA[4 * j + 2, i * 3 + j] = C;
                    inversedA[4 * j + 3, i * 3 + j] = D;
                }
            }
            return inversedA.Multiply(1 / (6 * Volume));
        }

        /// <summary>
        /// Метод, получающий коэффициенты геометрических характеристик
        /// </summary>
        /// <param name="index">Индекс узла</param>
        /// <returns>Коэффициенты геометрических характеристик</returns>
        private (double, double, double, double) GetABCD(int index)
        {
            (int i, int j, int m) = ShiftIndexes(index);
            double A = Math.Pow(-1, index) * Matrix.Determinant(new double[,]
            {
                { Nodes[i].X, Nodes[i].Y, Nodes[i].Z },
                { Nodes[j].X, Nodes[j].Y, Nodes[j].Z },
                { Nodes[m].X, Nodes[m].Y, Nodes[m].Z }
            });
            double B = Math.Pow(-1, index) * Matrix.Determinant(new double[,]
            {
                {1, Nodes[i].Y, Nodes[i].Z },
                {1, Nodes[j].Y, Nodes[j].Z },
                {1, Nodes[m].Y, Nodes[m].Z }
            });
            double C = Math.Pow(-1, index) * Matrix.Determinant(new double[,]
            {
                { Nodes[i].X, 1,Nodes[i].Z },
                { Nodes[j].X, 1,Nodes[j].Z },
                { Nodes[m].X, 1,Nodes[m].Z }
            });
            double D = Math.Pow(-1, index) * Matrix.Determinant(new double[,]
            {
                { Nodes[i].X, Nodes[i].Y, 1 },
                { Nodes[j].X, Nodes[j].Y, 1 },
                { Nodes[m].X, Nodes[m].Y, 1 }
            });
            return (A, B, C, D);
        }

        /// <summary>
        /// Выполняет круговую перестановку индексов
        /// </summary>
        /// <param name="index">Текущий индекс</param>
        /// <returns>Кортеж индексов после круговой перестановки</returns>
        private (int, int, int) ShiftIndexes(int index)
        {
            return ((index + 1) % 4, (index + 2) % 4, (index + 3) % 4);
        }

        /// <summary>
        /// Вычисляет эластичную матрицу линейного тетраэдра
        /// </summary>
        /// <param name="material">Материал линейного тетраэдра</param>
        /// <returns>Эластичная матрица линейного тетраэдра</returns>
        public override double[,] GetElasticMatrix(MaterialDTO material)
        {
            double ro = material.ShearModulus * 2 + material.LameCoefficient;
            return new double[,]
            {
                {ro, material.LameCoefficient, material.LameCoefficient, 0, 0, 0 },
                {material.LameCoefficient, ro, material.LameCoefficient, 0, 0, 0 },
                {material.LameCoefficient, material.LameCoefficient, ro, 0, 0, 0 },
                {0, 0, 0, material.ShearModulus, 0, 0 },
                {0, 0, 0, 0, material.ShearModulus, 0 },
                {0, 0, 0, 0, 0, material.ShearModulus }
            };
        }

        /// <summary>
        /// Метод копирует конечный элемент
        /// </summary>
        /// <param name="nodes">Узлы конечно-элементной сетки, в котором находится этот конечный элемент</param>
        /// <returns>Копия текущего конечного элемента</returns>
        public override ElementDTO Copy(IList<NodeDTO> nodes)
        {
            var copyNodes = Nodes.Select(node => nodes.First(n => n.Id == node.Id));
            return new LinearTetrahedronDTO(Id, copyNodes.ToArray(), Color);
        }
    }
}
