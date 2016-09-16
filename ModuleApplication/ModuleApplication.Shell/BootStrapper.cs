using System.Linq;
using System.Windows;
using log4net;
using Microsoft.Practices.ServiceLocation;
using Prism.Unity;
using Microsoft.Practices.Unity;
using ModuleApplication.Core.Tools;
using ModuleApplication.Main.Modules;
using Prism.Logging;
using Prism.Modularity;

namespace ModuleApplication.Shell
{
    public class BootStrapper : UnityBootstrapper
    {

        protected override ILoggerFacade CreateLogger()
        {
            return new Log4NetLogger();
        }

        private void LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            Logger.Log(string.Format("{0} has been loaded ({1})", e.ModuleInfo.ModuleName, e.ModuleInfo.State),Category.Debug, Priority.None);
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Views.Shell>();
        }

        protected override void InitializeModules()
        {
            var manager = ServiceLocator.Current.GetInstance<IModuleManager>();
            manager.LoadModuleCompleted += LoadModuleCompleted;

            base.InitializeModules();

            Logger.Log("Modules found : " + ModuleCatalog.Modules.Count(),Category.Debug, Priority.None);

            foreach (var module in ModuleCatalog.Modules)
            {
                Logger.Log(string.Format(" - {0} ({1})", module.ModuleName, module.State), Category.Debug, Priority.None);
            }

            Application.Current.MainWindow = (Views.Shell)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var catalog = new DirectoryModuleCatalog { ModulePath = @".\Modules" };
            return catalog;
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            var catalog = (ModuleCatalog)ModuleCatalog;

            // Main module
            catalog.AddModule(typeof(MainModule));

        }
    }
}
