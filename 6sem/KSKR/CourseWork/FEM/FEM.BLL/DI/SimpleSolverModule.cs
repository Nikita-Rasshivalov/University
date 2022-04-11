using FEM.BLL.Solvers;
using Ninject.Modules;

namespace FEM.BLL.DI
{
    /// <summary>
    /// Модуль для внедрения зависимостей для вычислителя метода конечных элементов
    /// </summary>
    public class SimpleSolverModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISolver>().To<SimpleMeshSolver>();
        }
    }
}
