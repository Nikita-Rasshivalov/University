namespace FEM.DAL.Data
{
    /// <summary>
    /// Перечисление, хранящее тип узла
    /// </summary>
    public enum NodeLoadType
    {
        Force,
        Fixed
    }
    /// <summary>
    /// Класс, отвечающий за нагрузку узла
    /// </summary>
    public class NodeLoad
    {
        /// <summary>
        /// Создание класса нагрузки узла
        /// </summary>
        /// <param name="id">Идентификатор узла, к которому прилагается нагрузка</param>
        /// <param name="type">Тип нагрузки узла</param>
        /// <param name="xForce">Сила, действующая на узел по оси X</param>
        /// <param name="yForce">Сила, действующая на узел по оси Y</param>
        /// <param name="zForce">Сила, действующая на узел по оси Z</param>
        public NodeLoad(int id, NodeLoadType type, double xForce, double yForce, double zForce)
        {
            Id = id;
            NodeLoadType = type;
            XForce = xForce;
            YForce = yForce;
            ZForce = zForce;
        }
        /// <summary>
        /// Идентификатор узла
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Тип нагрузки на узле
        /// </summary>
        public NodeLoadType NodeLoadType { get; set; }
        /// <summary>
        /// Сила по оси X
        /// </summary>
        public double XForce { get; set; }
        /// <summary>
        /// Сила по оси Y
        /// </summary>
        public double YForce { get; set; }
        /// <summary>
        /// Сила по оси Z
        /// </summary>
        public double ZForce { get; set; }
    }
}
