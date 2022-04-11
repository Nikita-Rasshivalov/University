using FEM.BLL.Data;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace FEM.BLL.ModelMakers
{
    /// <summary>
    /// Интерфейс создателя геометрии сетки
    /// </summary>
    public interface IMeshModelMaker
    {
        /// <summary>
        /// Метод создания геометрии сетки
        /// </summary>
        /// <param name="mesh">Конечно-элементная сетка</param>
        /// <param name="material">Материал сетки</param>
        /// <returns>Геометрия сетки</returns>
        /// <param name="loads"></param>
        public Model3DGroup GenerateGeometry(MeshDTO mesh, MaterialGroup material, IList<NodeLoadDTO> loads);

        /// <summary>
        /// Вычисляет модель с градиентным окрасом. Градиент показывает изменения характеристики элементов сетки
        /// </summary>
        /// <param name="mesh">Сетка, на основе который строится модель</param>
        /// <param name="elementsCharacteristic">Характеристика (Напряжение, деформации и т.д.) конечных элементов в этой сетке</param>
        /// <returns>Модель с градиентным окрасом в зависимости от характеристики</returns>
        public Model3DGroup GenerateCharacteristicModel(MeshDTO mesh, double[] elementsCharacteristic);
    }
}
