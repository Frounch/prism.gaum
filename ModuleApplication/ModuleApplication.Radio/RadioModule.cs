using System;
using ModuleApplication.Core.Regions;
using Prism.Logging;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleApplication.Radio
{
    [Module(ModuleName = "Radio", OnDemand = false)]
    public class RadioModule : IModule
    {
        private readonly ILoggerFacade m_Logger;
        private readonly IRegionManager m_RegionManager;

        public RadioModule(ILoggerFacade logger, IRegionManager regionManager)
        {
            m_RegionManager = regionManager;
            m_Logger = logger;
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            if (regionManager == null)
            {
                throw new ArgumentNullException("regionManager");
            }
        }

        public void Initialize()
        {
            m_RegionManager.RegisterViewWithRegion(RegionDefinition.MainMenuItemsRegion, typeof(Views.MainMenuItemView));
            m_Logger.Log("[RADIO] : Radio module initialized", Category.Debug, Priority.None);
        }
    }
}
