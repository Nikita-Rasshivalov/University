namespace FEM.DAL.Data
{
    /// <summary>
    /// Узел конечно-элементной сетки
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Создание узла
        /// </summary>
        /// <param name="id">Идентификатор узла</param>
        /// <param name="x">Координата x узла</param>
        /// <param name="y">Координата y узла</param>
        /// <param name="z">Координата z узла</param>
        public Node(int id, double x, double y, double z)
        {
            Id = id;
            X = x;
            Y = y;
            Z = z;
        }
        /// <summary>
        /// Идентификатор узла
        /// </summary>
        public int Id { get; }
        /// <summary>
        /// Координата x узла
        /// </summary>
        public double X { get; }
        /// <summary>
        /// Координата y узла
        /// </summary>
        public double Y { get; }
        /// <summary>
        /// Координата z узла
        /// </summary>
        public double Z { get; }
    }
}
