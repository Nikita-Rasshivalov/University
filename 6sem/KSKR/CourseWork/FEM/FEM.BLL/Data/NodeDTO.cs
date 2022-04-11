namespace FEM.BLL.Data
{
    /// <summary>
    /// Узел конечно-элементной сетки
    /// </summary>
    public class NodeDTO
    {
        /// <summary>
        /// Создание узла
        /// </summary>
        /// <param name="id">Идентификатор узла</param>
        /// <param name="x">Координата x узла</param>
        /// <param name="y">Координата y узла</param>
        /// <param name="z">Координата z узла</param>
        public NodeDTO(int id, double x, double y, double z)
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
        public double X { get; private set; }
        /// <summary>
        /// Координата y узла
        /// </summary>
        public double Y { get; private set; }
        /// <summary>
        /// Координата z узла
        /// </summary>
        public double Z { get; private set; }

        /// <summary>
        /// Создаёт копию текущего узла
        /// </summary>
        /// <returns>Копия текущего узла</returns>
        public NodeDTO Copy()
        {
            return new NodeDTO(Id, X, Y, Z);
        }

        /// <summary>
        /// Перемещает узел по трём координатам
        /// </summary>
        /// <param name="x">Смещение по координате x</param>
        /// <param name="y">Смещение по координате y</param>
        /// <param name="z">Смещение по координате z</param>
        public void Move(double x, double y, double z)
        {
            X += x;
            Y += y;
            Z += z;
        }
    }
}
