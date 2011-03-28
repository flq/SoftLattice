using System;
using SoftLattice.Common;
using SoftLattice.Core.Common;
using System.Linq;
using StructureMap;
using MemBus;

namespace SoftLattice.Core.Startup
{
    public class InstantiateAllSubscriberTypes : IStartup
    {
        private readonly KnownPluginDescriptors _pluginDescriptors;
        private readonly IContainer _container;
        private readonly ISubscriber _subscriber;

        public InstantiateAllSubscriberTypes(KnownPluginDescriptors pluginDescriptors, IContainer container, ISubscriber subscriber)
        {
            _pluginDescriptors = pluginDescriptors;
            _container = container;
            _subscriber = subscriber;
        }

        public void Start()
        {
            foreach (var type in _pluginDescriptors.SelectMany(pd => pd.MessageListenerTypes))
            {
                var subscriber = _container.GetInstance(type);
                _subscriber.Subscribe(subscriber);
            }
        }

        public StartupPhase StartupPhaseInWhichToRun
        {
            get { return StartupPhase.AllPluginsKnown; }
        }
    }
}