using System;
using System.Linq;

namespace FEM.DAL.Data.Elements
{
    /// <summary>
    /// Линейный тетраэдральный конечный элемент
    /// </summary>
    public class LinearTetrahedron : Element
    {
        /// <summary>
        /// Создание линейного тетраэдрального конечного элемента
        /// </summary>
        /// <param name="id">Идентификатор конечного элемента</param>
        /// <param name="nodeIndexes">Индексы узлов конечного элемента</param>
        public LinearTetrahedron(int id, int[] nodeIndexes) : base(id, nodeIndexes)
        {
            if (nodeIndexes.Distinct().Count() != 4)
            {
                throw new Exception("У линейного тетраэдра должно быть 4 различных узла");
            }
        }
    }
}
