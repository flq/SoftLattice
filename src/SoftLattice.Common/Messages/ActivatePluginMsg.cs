using System;

namespace SoftLattice.Common
{
    [Message]
    public class ActivatePluginMsg : ActivateViewModelMsg
    {
        private readonly string _pluginTitle;

        public ActivatePluginMsg(Type viewModelType, string pluginTitle) : base(viewModelType)
        {
            _pluginTitle = pluginTitle;
        }

        public string PluginTitle
        {
            get { return _pluginTitle; }
        }
    }
}