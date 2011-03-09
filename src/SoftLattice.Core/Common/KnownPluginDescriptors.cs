using System.Collections.Generic;
using MemBus;

namespace SoftLattice.Core.Common
{
    public class KnownPluginDescriptors
    {
        private readonly IBus _bus;
        static readonly List<PluginDescriptor> _plugins = new List<PluginDescriptor>();

        public KnownPluginDescriptors(IBus bus)
        {
            _bus = bus;
        }

        internal void Add(PluginDescriptor pluginDescriptor)
        {
            _plugins.Add(pluginDescriptor);
        }
    }
}