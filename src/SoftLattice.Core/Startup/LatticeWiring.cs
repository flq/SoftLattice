using System;
using System.Reflection;
using MemBus;
using SoftLattice.Common;
using SoftLattice.Core.Common;

namespace SoftLattice.Core.Startup
{
    public class LatticeWiring : ILatticeWiring
    {
        private readonly IBus _bus;
        private readonly PluginDescriptor _pluginDescriptor;

        public LatticeWiring(IBus bus, PluginDescriptor pluginDescriptor)
        {
            _bus = bus;
            _pluginDescriptor = pluginDescriptor;
        }

        void ILatticeWiring.RegisterMessageListener(object listener)
        {
            _bus.Subscribe(listener);
        }

        void ILatticeWiring.AddResource(string relativePath)
        {
            _pluginDescriptor.AddResource(relativePath);
        }

        public void AddResources(Func<string, bool> pathPredicate)
        {
            _pluginDescriptor.AddResource(pathPredicate);
        }

        internal PluginDescriptor ConstructedPluginDescriptor { get { return _pluginDescriptor;  } }

        internal void SetPluginAssembly(Assembly assembly)
        {
            _pluginDescriptor.SetPluginAssembly(assembly);
        }
    }
}