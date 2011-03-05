using System;
using System.Collections.Generic;
using MemBus;
using SoftLattice.Common;
using System.Linq;

namespace SoftLattice.Core.Common
{
    public class KnownPluginDescriptors
    {
        private readonly IBus _bus;
        static readonly List<PluginDescriptor> _plugins = new List<PluginDescriptor>();

        public KnownPluginDescriptors(IBus bus)
        {
            _bus = bus;
            bus.Subscribe<StartupMsg>(onStartup);
        }

        private void onStartup(StartupMsg obj)
        {
            var pd = _plugins.FirstOrDefault(p => p.TypeOfStartupView != null);
            if (pd != null)
                _bus.Publish(new ActivateViewModelMsg(pd.TypeOfStartupView));
        }

        internal void Add(PluginDescriptor pluginDescriptor)
        {
            _plugins.Add(pluginDescriptor);
        }
    }
}