using System;
using System.Reflection;
using MemBus;
using SoftLattice.Common;
using SoftLattice.Core.Common;
using StructureMap;

namespace SoftLattice.Core.Startup
{
    public class LatticeWiring : ILatticeWiring
    {
        private readonly IBus _bus;
        private readonly IContainer _container;
        private readonly PluginDescriptor _pluginDescriptor;

        public LatticeWiring(IBus bus, IContainer container, PluginDescriptor pluginDescriptor)
        {
            _bus = bus;
            _container = container;
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

        public void RegisterFloatingViewModel<ViewModel>(string modelKey)
        {
            _container.Configure(e=>e.For<object>().Use<ViewModel>().Named(modelKey));
        }

        internal PluginDescriptor ConstructedPluginDescriptor { get { return _pluginDescriptor;  } }

        internal void SetPluginAssembly(Assembly assembly)
        {
            _pluginDescriptor.SetPluginAssembly(assembly);
        }
    }
}