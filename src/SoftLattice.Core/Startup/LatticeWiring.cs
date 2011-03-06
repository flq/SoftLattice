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

        void ILatticeWiring.RegisterStartupView<T>()
        {
            _pluginDescriptor.AddStartupView<T>();
        }

        void ILatticeWiring.AddResource(string relativePath)
        {
            _pluginDescriptor.AddResource(relativePath);
        }

        internal PluginDescriptor ConstructedPluginDescriptor { get { return _pluginDescriptor;  } }

        internal void SetPluginAssembly(Assembly assembly)
        {
            _pluginDescriptor.SetPluginAssembly(assembly);
        }
    }
}