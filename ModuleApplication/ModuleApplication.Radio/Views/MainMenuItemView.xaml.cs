using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Unity;
using ModuleApplication.Core.Regions;
using Prism.Regions;

namespace ModuleApplication.Radio.Views
{
    /// <summary>
    /// Interaction logic for MainMenuItemView.xaml
    /// </summary>
    public partial class MainMenuItemView : UserControl
    {
        private readonly IRegionManager m_RegionManager;
        private readonly IUnityContainer m_Container;

        public MainMenuItemView(IRegionManager regionManager, IUnityContainer container)
        {
            m_Container = container;
            m_RegionManager = regionManager;
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var mainRegion = m_RegionManager.Regions[RegionDefinition.MainContentRegion];
            if (mainRegion == null) return;

            var view = mainRegion.GetView("RadioMainView") as RadioMainView;
            if (view == null)
            {
                view = m_Container.Resolve<RadioMainView>();
                mainRegion.Add(view, "RadioMainView");
            }
            mainRegion.Activate(view);
        }
    }
}
