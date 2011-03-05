using System;
using System.Collections.Generic;
using SoftLattice.Common;
using SoftLattice.Core.Common;

namespace SoftLattice.Core.Startup
{
    public class WireUpPlugins : IStartup
    {
        private readonly IEnumerable<ILatticeGroup> _groups;
        private readonly Func<LatticeWiring> _wiring;
        private readonly KnownPluginDescriptors _knownPluginDescriptors;

        public WireUpPlugins(IEnumerable<ILatticeGroup> groups, Func<LatticeWiring> wiring, KnownPluginDescriptors knownPluginDescriptors)
        {
            _groups = groups;
            _wiring = wiring;
            _knownPluginDescriptors = knownPluginDescriptors;
        }

        public void Start()
        {
            foreach (var grp in _groups)
            {
                var wiring = _wiring();
                grp.Access(wiring);
                _knownPluginDescriptors.Add(wiring.ConstructedPluginDescriptor);
            }
        }

        public StartupPhase StartupPhaseInWhichToRun
        {
            get { return StartupPhase.IndependentInfrastructure; }
        }
    }
}