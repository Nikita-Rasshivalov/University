using FEM.BLL.DI;
using Microsoft.Extensions.Configuration;
using Ninject;
using Ninject.Modules;
using System.IO;
using System.Windows;

namespace FEM.WPFGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            IConfigurationRoot config = GetConfig();
            //BLL dependencies
            NinjectModule geometryMakerModule = new SimpleModelMakerModule();
            NinjectModule meshLoaderModule = new MeshTxtLoaderModule(config["NodesTxt"], config["ElementsTxt"]);
            NinjectModule nodeLoadsLoaderModule = new NodeLoadsCsvLoaderModule(config["NodeLoadsCsv"]);
            NinjectModule materialsLoaderModule = new MaterialsLoaderModule(config["MaterialsCsv"]);
            NinjectModule solver = new SimpleSolverModule();

            _container = new StandardKernel(geometryMakerModule, meshLoaderModule, nodeLoadsLoaderModule, materialsLoaderModule, solver);
        }

        private IConfigurationRoot GetConfig()
        {
            var curDir = Directory.GetCurrentDirectory();
            var baseDir = Directory.GetParent(curDir).Parent.Parent;

            var config = new ConfigurationBuilder()
                .AddJsonFile(@$"{baseDir}\config.json")
                .Build();

            return config;
        }

        private void ComposeObjects()
        {
            Current.MainWindow = _container.Get<MainWindow>();
            Current.MainWindow.Title = "FEM";
        }
    }
}
