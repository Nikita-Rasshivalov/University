using FEM.DAL.Data;
using FEM.DAL.Loaders;
using Ninject.Modules;
using System.Collections.Generic;

namespace FEM.BLL.DI
{
    /// <summary>
    /// Модуль для внедрения зависимостей, привязывающий к загрузчику материалов загрузчик из csv файла
    /// </summary>
    public class MaterialsLoaderModule : NinjectModule
    {
        string _pathToMaterials;
        /// <summary>
        /// Создание модуля внедрения зависимостей
        /// </summary>
        /// <param name="pathToMaterials">Путь к csv файлу с материалами</param>
        public MaterialsLoaderModule(string pathToMaterials)
        {
            _pathToMaterials = pathToMaterials;
        }
        public override void Load()
        {
            Bind<ILoader<IList<Material>>>()
                .To<MaterialsCsvLoader>()
                .WithConstructorArgument("pathToCsv", _pathToMaterials);
        }
    }
}
