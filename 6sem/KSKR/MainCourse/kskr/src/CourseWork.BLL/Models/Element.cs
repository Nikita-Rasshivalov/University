using System;
using System.Collections.Generic;
using System.Linq;
using Accord.Math;

namespace CourseWork.BLL.Models
{
    public class Element : ICloneable
    {
        public ElementType ElementType { get; set; } = ElementType.Tet4;

        public int Id { get; set; }

        public List<Node> Nodes { get; set; }

        /// <summary>
        /// Объём конечного элемента
        /// </summary>
        public double Volume
        {
            get
            {
                return Math.Abs(Matrix.Determinant(
                new double[,]
                {
                    {1,Nodes[0].Position.X, Nodes[0].Position.Y, Nodes[0].Position.Z},
                    {1,Nodes[1].Position.X, Nodes[1].Position.Y, Nodes[1].Position.Z},
                    {1,Nodes[2].Position.X, Nodes[2].Position.Y, Nodes[2].Position.Z},
                    {1,Nodes[3].Position.X, Nodes[3].Position.Y, Nodes[3].Position.Z}
                }) / 6);
            }
        }

        public (int i, int j, int m) getBias(int index)
        {
            var i = (index + 1) < 4 ? index + 1 : index + 1 - 4;
            var j = (index + 2) < 4 ? index + 2 : index + 2 - 4;
            var m = (index + 3) < 4 ? index + 3 : index + 3 - 4;
            return (i, j, m);
        }

        public double get_b(int index) {
            var (i, j, m) = getBias(index);
            var b = new double[,]
                {
                    {1, Nodes[i].Position.Y, Nodes[i].Position.Z },
                    {1, Nodes[j].Position.Y, Nodes[j].Position.Z },
                    {1, Nodes[m].Position.Y, Nodes[m].Position.Z },
                };
        return Matrix.Determinant(b);
        }

        public double get_c(int index)
        {
            var (i, j, m) = getBias(index);
            var c = new double[,]
                {
                    {Nodes[i].Position.X, 1, Nodes[i].Position.Z },
                    {Nodes[j].Position.X, 1, Nodes[j].Position.Z },
                    {Nodes[m].Position.X, 1, Nodes[m].Position.Z },
                };
            return Matrix.Determinant(c);
        }

        public double get_d(int index)
        {
            var (i, j, m) = getBias(index);
            var d = new double[,]
                {
                    {Nodes[i].Position.X, Nodes[i].Position.Y, 1 },
                    {Nodes[j].Position.X, Nodes[j].Position.Y, 1 },
                    {Nodes[m].Position.X, Nodes[m].Position.Y, 1 },
                };
            return Matrix.Determinant(d);
        }

        public double[,] GeometryMatrix()
        {
            var bi = get_b(0);
            var ci = get_c(0);
            var di = get_d(0);
            var bj = get_b(1);
            var cj = get_c(1);
            var dj = get_d(1);
            var bm = get_b(2);
            var cm = get_c(2);
            var dm  = get_d(2);
            var bn = get_b(3);
            var cn = get_c(3);
            var dn = get_d(3);
            var b = new double[,]
            {
                    {bi, 0, 0, bj, 0, 0, bm, 0, 0, bn, 0, 0},
                    {0, ci, 0, 0, cj, 0, 0, cm, 0, 0, cn, 0},
                    {0, 0, di, 0, 0, dj, 0, 0, dm, 0, 0, dn},
                    {ci, bi, 0, cj, bj, 0, cm, bm, 0, cn, bn, 0},
                    {0, di, ci, 0, dj, cj, 0, dm, cm, 0, dn, cn},
                    {di, 0, bi, dj, 0, bj, dm, 0, bm, dn, 0, bn},
            };
            return b.Divide(6 * Volume);
        }

        //public double[,] getElastic(DetailMaterial material)
        //{
        //    var k1 = material.PoissonCoefficient / (1 - material.PoissonCoefficient);
        //    var k2 = (1 - 2 * material.PoissonCoefficient) / (2 * (1 - material.PoissonCoefficient));

        //    var D = new double[,]
        //    {
        //            {1, k1, k1, 0, 0, 0},
        //            {k1, 1, k1, 0, 0, 0},
        //            {k1, k1, 1, 0, 0, 0},
        //            {0, 0, 0, k2, 0, 0},
        //            {0, 0, 0, 0, k2, 0},
        //            {0, 0, 0, 0, 0, k2},
        //    };
        //    return D.Multiply(material.YoungModulus * (1 - material.PoissonCoefficient) / ((1 + material.PoissonCoefficient) * (1 - 2 * material.PoissonCoefficient)));

        //}
        public double[,] getElastic(DetailMaterial material)
        {
            var k1 = material.YoungModulus / (1 + material.PoissonCoefficient);
            var k2 = (1 - material.PoissonCoefficient) / (1 - 2 * material.PoissonCoefficient);
            var k3 = material.PoissonCoefficient / (1 - 2 * material.PoissonCoefficient);
            var D = new double[,]
            {
                    {k2, k3, k3, 0, 0, 0},
                    {k3, k2, k3, 0, 0, 0},
                    {k3, k3, k2, 0, 0, 0},
                    {0, 0, 0, 0.5, 0, 0},
                    {0, 0, 0, 0, 0.5, 0},
                    {0, 0, 0, 0, 0, 0.5},
            };
            return D.Multiply(k1);
        }

        public double[,] GetLocalStiffnessMatrix(DetailMaterial material)
        {
            return GeometricMatrix.Transpose().Dot(getElastic(material)).Dot(GeometricMatrix).Multiply(Volume);
        }

        ///// <summary>
        ///// Вычисление матрицы жесткости линейного тетраэдра
        ///// </summary>
        ///// <param name="material">Материал тетраэдра</param>
        ///// <returns>Матрица жесткости линейного тетраэдра</returns>
        //public double[,] GetLocalStiffnessMatrix(DetailMaterial material)
        //{
        //    return GeometricMatrix.Transpose().Dot(getElastic(material)).Dot(GeometricMatrix).Multiply(Volume);
        //}

        /// <summary>
        /// Геометрическая матрица конечного элемента [D]
        /// </summary>
        public double[,] GeometricMatrix
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

        /// <summary>
        /// Вычисляет обратную матрицу А для вычисления геометрической матрицы
        /// </summary>
        /// <returns>Обратная матрица А</returns>
        public double[,] GetInversedA()
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
                { Nodes[i].Position.X, Nodes[i].Position.Y, Nodes[i].Position.Z },
                { Nodes[j].Position.X, Nodes[j].Position.Y, Nodes[j].Position.Z },
                { Nodes[m].Position.X, Nodes[m].Position.Y, Nodes[m].Position.Z }
            });
            double B = Math.Pow(-1, index) * Matrix.Determinant(new double[,]
            {
                {1, Nodes[i].Position.Y, Nodes[i].Position.Z },
                {1, Nodes[j].Position.Y, Nodes[j].Position.Z },
                {1, Nodes[m].Position.Y, Nodes[m].Position.Z }
            });
            double C = Math.Pow(-1, index) * Matrix.Determinant(new double[,]
            {
                { Nodes[i].Position.X, 1,Nodes[i].Position.Z },
                { Nodes[j].Position.X, 1,Nodes[j].Position.Z },
                { Nodes[m].Position.X, 1,Nodes[m].Position.Z }
            });
            double D = Math.Pow(-1, index) * Matrix.Determinant(new double[,]
            {
                { Nodes[i].Position.X, Nodes[i].Position.Y, 1 },
                { Nodes[j].Position.X, Nodes[j].Position.Y, 1 },
                { Nodes[m].Position.X, Nodes[m].Position.Y, 1 }
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
        public double[,] GetElasticMatrix(DetailMaterial material)
        {
            double ro = material.ShiftModulus * 2 + material.LameCoefficient;
            return new double[,]
            {
                {ro, material.LameCoefficient, material.LameCoefficient, 0, 0, 0 },
                {material.LameCoefficient, ro, material.LameCoefficient, 0, 0, 0 },
                {material.LameCoefficient, material.LameCoefficient, ro, 0, 0, 0 },
                {0, 0, 0, material.ShiftModulus, 0, 0 },
                {0, 0, 0, 0, material.ShiftModulus, 0 },
                {0, 0, 0, 0, 0, material.ShiftModulus }
            };
        }

        public override string ToString()
        {
            return string.Join("\n", Nodes);
        }

        public object Clone()
        {
            return new Element
            { 
                Id = Id,
                ElementType = ElementType,
                Nodes = Nodes.Select(n => (Node)n.Clone()).ToList(),
            };
        }
    }
}
