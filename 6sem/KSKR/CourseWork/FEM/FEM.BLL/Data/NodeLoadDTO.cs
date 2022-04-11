namespace FEM.BLL.Data
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
    public class NodeLoadDTO
    {
        public NodeLoadDTO()
        {

        }
        /// <summary>
        /// Создание класса нагрузки узла
        /// </summary>
        /// <param name="id">Идентификатор узла, к которому прилагается нагрузка</param>
        /// <param name="type">Тип нагрузки узла</param>
        /// <param name="force">Вектор нагрузки на узел</param>
        public NodeLoadDTO(int id, NodeLoadType type, NodeForceVector force)
        {
            Id = id;
            NodeLoadType = type;
            ForceVector = force;
        }
        /// <summary>
        /// Идентификатор узла
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Тип нагрузки на узле
        /// </summary>
        public NodeLoadType NodeLoadType { get; set; }
        public NodeForceVector ForceVector { get; set; }
    }
}
