namespace FEM.DAL.Data
{
    /// <summary>
    /// Конечный элемент
    /// </summary>
    public abstract class Element
    {
        /// <summary>
        /// Создание конечного элемента
        /// </summary>
        /// <param name="id">Идентификатор элемента</param>
        /// <param name="nodeIndexes">Индексы узлов конечного элемента</param>
        public Element(int id, int[] nodeIndexes)
        {
            Id = id;
            NodeIndexes = nodeIndexes;
        }
        /// <summary>
        /// Идентификатор конечного элемента
        /// </summary>
        public int Id { get; }
        /// <summary>
        /// Узлы конечного элемента
        /// </summary>
        public int[] NodeIndexes { get; }
    }
}
