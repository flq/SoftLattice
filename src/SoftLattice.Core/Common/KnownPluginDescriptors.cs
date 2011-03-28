using System;
using System.Collections;
using System.Collections.Generic;
using MemBus;

namespace SoftLattice.Core.Common
{
    public class KnownPluginDescriptors : IEnumerable<PluginDescriptor>
    {
        static readonly List<PluginDescriptor> _plugins = new List<PluginDescriptor>();
        
        internal void Add(PluginDescriptor pluginDescriptor)
        {
            _plugins.Add(pluginDescriptor);
        }

        public IEnumerator<PluginDescriptor> GetEnumerator()
        {
            return _plugins.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}