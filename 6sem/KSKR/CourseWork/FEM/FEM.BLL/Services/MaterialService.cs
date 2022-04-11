using FEM.BLL.Data;
using FEM.DAL.Data;
using FEM.DAL.Loaders;
using System.Collections.Generic;

namespace FEM.BLL.Services
{
    /// <summary>
    /// Сервис для получения сущностей материалов
    /// </summary>
    public class MaterialService
    {
        private readonly ILoader<IList<Material>> _materialsLoader;
        /// <summary>
        /// Создание сервиса для получения сущностей материалов
        /// </summary>
        /// <param name="materialsLoader">Загрузчик материалов</param>
        public MaterialService(ILoader<IList<Material>> materialsLoader)
        {
            _materialsLoader = materialsLoader;
        }
        /// <summary>
        /// Список материалов детали
        /// </summary>
        public IList<MaterialDTO> Materials
        {
            get
            {
                IList<MaterialDTO> materialsDTO = new List<MaterialDTO>();
                var materials = _materialsLoader.Load();
                foreach (var material in materials)
                {
                    materialsDTO.Add(new MaterialDTO(material.Id, material.Name, material.Color, material.YoungModulus, material.PuassonsCoefficient));
                }
                return materialsDTO;
            }
        }
    }
}
