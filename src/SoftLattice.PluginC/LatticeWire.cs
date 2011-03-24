using System;
using SoftLattice.Common;

namespace SoftLattice.PluginC
{
    public class LatticeWire : ILatticeGroup
    {
        private readonly IPublishMessage _publisher;

        public LatticeWire(IPublishMessage publisher)
        {
            _publisher = publisher;
        }

        public void Access(ILatticeWiring wiring)
        {
            wiring.RegisterMessageListener(this);
            wiring.AddResources(s=>s.Contains("resources"));
        }

        public void Handle(StartupMsg startup)
        {
            _publisher.Publish(new ActivatePluginMsg(typeof(PluginCViewModel), "Plugin C, Aye!"));
        }
    }
}