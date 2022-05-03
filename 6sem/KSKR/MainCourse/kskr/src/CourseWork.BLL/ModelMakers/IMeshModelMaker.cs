using CourseWork.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace CourseWork.BLL.ModelMakers
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
        public Model3DGroup GenerateGeometry(FiniteElementModel mesh, MaterialGroup material);

        /// <summary>
        /// Вычисляет модель с градиентным окрасом. Градиент показывает изменения характеристики элементов сетки
        /// </summary>
        /// <param name="mesh">Сетка, на основе который строится модель</param>
        /// <param name="elementsCharacteristic">Характеристика (Напряжение, деформации и т.д.) конечных элементов в этой сетке</param>
        /// <returns>Модель с градиентным окрасом в зависимости от характеристики</returns>
        public Model3DGroup GenerateCharacteristicModel(FiniteElementModel mesh, double[] elementsCharacteristic);
    }
}
