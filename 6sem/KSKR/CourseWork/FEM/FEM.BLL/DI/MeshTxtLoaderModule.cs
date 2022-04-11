using FEM.DAL.Data;
using FEM.DAL.Loaders;
using Ninject.Modules;

namespace FEM.BLL.DI
{
    /// <summary>
    /// Модуль для внедрения зависимостей, привязывающий к загрузчику сетки загрузчик из txt файла
    /// </summary>
    public class MeshTxtLoaderModule : NinjectModule
    {
        string _pathToNodes;
        string _pathToElements;
        /// <summary>
        /// Создание модуля для внедрения зависимостей, привязывающий к загрузчику сетки загрузчик из txt файла
        /// </summary>
        /// <param name="pathToNodes">Путь к txt файлу с узлами</param>
        /// <param name="pathToElements">Путь к txt файлу с элементами</param>
        public MeshTxtLoaderModule(string pathToNodes, string pathToElements)
        {
            _pathToNodes = pathToNodes;
            _pathToElements = pathToElements;
        }
        public override void Load()
        {
            Bind<ILoader<Mesh>>()
                .To<MeshTxtLoader>()
                .WithConstructorArgument("pathToNodes", _pathToNodes)
                .WithConstructorArgument("pathToElements", _pathToElements);
        }
    }
}
