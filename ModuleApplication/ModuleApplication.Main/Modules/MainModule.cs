using Microsoft.Practices.Unity;
using ModuleApplication.Core.Regions;
using ModuleApplication.Main.Views;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleApplication.Main.Modules
{
    public class MainModule : IModule
    {
        private readonly IRegionManager m_RegionManager;
        private readonly IUnityContainer m_Container;

        public MainModule(IRegionManager regionManager, IUnityContainer container)
        {
            m_Container = container;
            m_RegionManager = regionManager;
        }

        public void Initialize()
        {
            m_RegionManager.RegisterViewWithRegion(RegionDefinition.StatusContentRegion, typeof(StatusView));
            m_RegionManager.RegisterViewWithRegion(RegionDefinition.HomeContentRegion, typeof(HomeView));
            var mainRegion = m_RegionManager.Regions[RegionDefinition.MainContentRegion];
            if (mainRegion == null) return;

            var view = mainRegion.GetView("MainView") as MainView;
            if (view == null)
            {
                view = m_Container.Resolve<MainView>();
                mainRegion.Add(view, "MainView");
            }
            mainRegion.Activate(view);
        }
    }
}
