using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Media3D;

namespace FEM.BLL.Data
{
    /// <summary>
    /// Конечный элемент
    /// </summary>
    public abstract class ElementDTO
    {
        /// <summary>
        /// Создание конечного элемента
        /// </summary>
        /// <param name="id">Идентификатор элемента</param>
        /// <param name="nodes">Узлы конечного элемента</param>
        /// <param name="color">Цвет конечного элемента</param>
        public ElementDTO(int id, NodeDTO[] nodes, Color color)
        {
            Id = id;
            Nodes = nodes;
            Color = color;
        }
        /// <summary>
        /// Идентификатор конечного элемента
        /// </summary>
        public int Id { get; }
        /// <summary>
        /// Узлы конечного элемента
        /// </summary>
        public NodeDTO[] Nodes { get; }
        /// <summary>
        /// Цвет конечного элемента
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// Получает локальную матрицу жесткости конечного элемента
        /// </summary>
        /// <param name="material">Материал конечного элемента</param>
        /// <returns>Локальная матрица жесткости конечного элемента</returns>
        public abstract double[,] GetStiffnessMatrix(MaterialDTO material);
        /// <summary>
        /// Создаёт копию экземпляра конечного элемента
        /// </summary>
        /// <param name="nodes">Узлы конечно-элементной сетки</param>
        /// <returns>Копия экземпляра конечного элемента</returns>
        public abstract ElementDTO Copy(IList<NodeDTO> nodes);

        /// <summary>
        /// Объём конечного элемента
        /// </summary>
        public abstract double Volume { get; }
        /// <summary>
        /// Центральная точка конечного элемента
        /// </summary>
        public abstract Point3D CenterPoint { get; }

        public double GetDistanceTo(ElementDTO other)
        {
            return Math.Sqrt(Math.Pow(this.CenterPoint.X - other.CenterPoint.X, 2) + Math.Pow(this.CenterPoint.Y - other.CenterPoint.Y, 2) + Math.Pow(this.CenterPoint.Z - other.CenterPoint.Z, 2));
        }

        /// <summary>
        /// Вычисляет эластичную матрицу конечного элемента
        /// </summary>
        /// <param name="material">Материал линейного тетраэдра</param>
        /// <returns>Эластичная матрица линейного тетраэдра</returns>
        public abstract double[,] GetElasticMatrix(MaterialDTO material);
        /// <summary>
        /// Вычисляет обратную матрицу А для вычисления геометрической матрицы
        /// </summary>
        /// <returns>Обратная матрица А</returns>
        public abstract double[,] GetInversedA();
        /// <summary>
        /// Геометрическая матрица конечного элемента [D]
        /// </summary>
        public abstract double[,] GeometricMatrix { get; }
    }
}
