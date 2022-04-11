using FEM.DAL.Data;
using FEM.DAL.Loaders;
using Ninject.Modules;
using System.Collections.Generic;

namespace FEM.BLL.DI
{
    /// <summary>
    /// Модуль для внедрения зависимостей, привязывающий к загрузчику узловых нагрузок загрузчик из csv файла
    /// </summary>
    public class NodeLoadsCsvLoaderModule : NinjectModule
    {
        string _pathToLoads;
        /// <summary>
        /// Создание модуля для внедрения зависимостей, привязывающий к загрузчику узловых нагрузок загрузчик из csv файла
        /// </summary>
        /// <param name="pathToLoads">Путь к csv файлу с узловыми нагрузками</param>
        public NodeLoadsCsvLoaderModule(string pathToLoads)
        {
            _pathToLoads = pathToLoads;
        }
        public override void Load()
        {
            Bind<ILoader<IList<NodeLoad>>>()
                .To<NodeLoadsCsvLoader>()
                .WithConstructorArgument("pathToCsv", _pathToLoads);
        }
    }
}
