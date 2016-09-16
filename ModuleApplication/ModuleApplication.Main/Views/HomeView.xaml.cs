using System;
using System.Windows.Controls;
using Microsoft.Practices.Unity;
using ModuleApplication.Core.Regions;
using Prism.Regions;

namespace ModuleApplication.Main.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        private readonly IRegionManager m_RegionManager;
        private readonly IUnityContainer m_Container;

        public HomeView(IRegionManager regionManager,IUnityContainer container)
        {
            m_Container = container;
            m_RegionManager = regionManager;
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
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
