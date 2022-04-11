namespace FEM.BLL.Data
{
    /// <summary>
    /// Вектор узлового усилия
    /// </summary>
    public class NodeForceVector
    {
        /// <summary>
        /// Создание вектора узлового усилия
        /// </summary>
        /// <param name="x">Усилие по координате X</param>
        /// <param name="y">Усилие по координате Z</param>
        /// <param name="z">Усилие по координате Y</param>
        public NodeForceVector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        /// <summary>
        /// Усилие по координате X
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Усилие по координате Y
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// Усилие по координате Z
        /// </summary>
        public double Z { get; set; }
    }
}
