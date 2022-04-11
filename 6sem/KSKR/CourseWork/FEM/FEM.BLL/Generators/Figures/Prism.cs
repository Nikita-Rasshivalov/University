using FEM.BLL.Data;
using System;
using System.Collections.Generic;

namespace FEM.BLL.MeshGenerators.Figures
{
    public class Prism
    {
        /// <summary>
        /// Количество сторон призмы
        /// </summary>
        public int SidesCount { get; set; }
        /// <summary>
        /// Длина стороны призмы
        /// </summary>
        public double SidesLength { get; set; }
        /// <summary>
        /// Высота призмы
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// Радиус описанной окружности
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// Создание n-угольной призмы
        /// </summary>
        /// <param name="sidesCount">Количество сторон призмы</param>
        /// <param name="sidesLength">Длина стороны призмы</param>
        /// <param name="height">Высота призмы</param>
        public Prism(int sidesCount, double sidesLength, double height)
        {
            SidesCount = sidesCount;
            SidesLength = sidesLength;
            Height = height;

            double angle = 360.0 / sidesCount;
            Radius = SidesLength / (2 * Math.Sin(Math.PI * angle / 180.0 / 2));
        }

        /// <summary>
        /// Создание 16 угольной призмы, заменяющей собой цилиндр
        /// </summary>
        /// <param name="radius">Радиус описанной вокруг призмы окружности</param>
        /// <param name="height">Высота призмы</param>
        public Prism(double radius, double height)
        {
            SidesCount = 16;

            //Вычисление радиуса призмы
            Radius = Math.Sqrt(2 * Math.PI * (Math.Pow(radius, 2) / (SidesCount * Math.Sin(Math.PI * (360.0 / SidesCount) / 180.0))));

            SidesLength = Math.Sqrt(2 * Math.Pow(Radius, 2) - 4 * Radius * Math.Cos(360.0 / SidesCount));
            Height = height;
        }

        /// <summary>
        /// Разбивает призму на узлы и треугольные призмы
        /// </summary>
        /// <returns>Узлы и треугольные призмы</returns>
        public (IList<TrianglePrism>, IList<NodeDTO>) GetTrianglePrismsWithNodes()
        {
            IList<TrianglePrism> prisms = new List<TrianglePrism>();
            IList<NodeDTO> nodes = new List<NodeDTO>();

            double angle = 360.0 / SidesCount;

            // Центральные узлы призмы
            nodes.Add(new NodeDTO(0, 0, 0, 0));
            nodes.Add(new NodeDTO(1, 0, Height / 4, 0));
            nodes.Add(new NodeDTO(2, 0, Height / 2, 0));
            nodes.Add(new NodeDTO(3, 0, -Height / 4, 0));
            nodes.Add(new NodeDTO(4, 0, -Height / 2, 0));

            // Узлы начала отсчёта призмы
            nodes.Add(new NodeDTO(5, Radius, 0, 0));
            nodes.Add(new NodeDTO(6, Radius, Height / 4, 0));
            nodes.Add(new NodeDTO(7, Radius, Height / 2, 0));
            nodes.Add(new NodeDTO(8, Radius, -Height / 4, 0));
            nodes.Add(new NodeDTO(9, Radius, -Height / 2, 0));

            double currentAngle = angle;
            int currentNodeId = 10;
            int currentPrismId = 0;

            while (currentAngle < 360)
            {
                double x = Radius * Math.Cos(Math.PI * currentAngle / 180.0);
                double z = Radius * Math.Sin(Math.PI * currentAngle / 180.0);

                nodes.Add(new NodeDTO(currentNodeId, x, 0, z));
                nodes.Add(new NodeDTO(currentNodeId + 1, x, Height / 4, z));
                nodes.Add(new NodeDTO(currentNodeId + 2, x, Height / 2, z));
                nodes.Add(new NodeDTO(currentNodeId + 3, x, -Height / 4, z));
                nodes.Add(new NodeDTO(currentNodeId + 4, x, -Height / 2, z));

                prisms.Add(new TrianglePrism(currentPrismId, new NodeDTO[] { nodes[0], nodes[1], nodes[currentNodeId - 5], nodes[currentNodeId - 4], nodes[currentNodeId], nodes[currentNodeId + 1] }));
                prisms.Add(new TrianglePrism(currentPrismId + 1, new NodeDTO[] { nodes[0], nodes[3], nodes[currentNodeId - 5], nodes[currentNodeId - 2], nodes[currentNodeId], nodes[currentNodeId + 3] }));
                prisms.Add(new TrianglePrism(currentPrismId + 2, new NodeDTO[] { nodes[1], nodes[2], nodes[currentNodeId - 4], nodes[currentNodeId - 3], nodes[currentNodeId + 1], nodes[currentNodeId + 2] }));
                prisms.Add(new TrianglePrism(currentPrismId + 3, new NodeDTO[] { nodes[3], nodes[4], nodes[currentNodeId - 2], nodes[currentNodeId - 1], nodes[currentNodeId + 3], nodes[currentNodeId + 4] }));

                currentNodeId += 5;
                currentPrismId += 4;
                currentAngle += angle;
            }

            prisms.Add(new TrianglePrism(currentPrismId, new NodeDTO[] { nodes[0], nodes[1], nodes[currentNodeId - 5], nodes[currentNodeId - 4], nodes[5], nodes[6] }));
            prisms.Add(new TrianglePrism(currentPrismId + 1, new NodeDTO[] { nodes[0], nodes[3], nodes[currentNodeId - 5], nodes[currentNodeId - 2], nodes[5], nodes[8] }));
            prisms.Add(new TrianglePrism(currentPrismId + 2, new NodeDTO[] { nodes[1], nodes[2], nodes[currentNodeId - 4], nodes[currentNodeId - 3], nodes[6], nodes[7] }));
            prisms.Add(new TrianglePrism(currentPrismId + 3, new NodeDTO[] { nodes[3], nodes[4], nodes[currentNodeId - 2], nodes[currentNodeId - 1], nodes[8], nodes[9] }));

            return (prisms, nodes);
        }
    }
}
