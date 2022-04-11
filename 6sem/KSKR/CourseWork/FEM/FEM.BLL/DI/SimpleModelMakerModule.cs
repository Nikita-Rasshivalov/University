using FEM.BLL.ModelMakers;
using Ninject.Modules;

namespace FEM.BLL.DI
{
    /// <summary>
    /// Модуль для внедрения зависимостей, привязывающий к создателю геометрии сетки обычный создатель геометрии
    /// </summary>
    public class SimpleModelMakerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMeshModelMaker>()
                .To<SimpleModelMaker>();
        }
    }
}
