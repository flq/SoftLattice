using System;
using MemBus;
using SoftLattice.Common;
using SoftLattice.Core.ApplicationShell;

namespace SoftLattice.Core.Startup
{
    /// <summary>
    /// The viewActivationPump could implement IHandles but we want it to be contravariant
    /// to be able to also handle specializations of <see cref="ActivateViewModelMsg"/>. The standard subscribe way
    /// of MemBus allows for such a behaviour.
    /// </summary>
    
    public class WireUpInfrastructure : IStartup
    {
        private readonly ISubscriber _subscriber;
        private readonly ViewActivationPump _pump;

        public WireUpInfrastructure(ISubscriber subscriber, ViewActivationPump pump)
        {
            _subscriber = subscriber;
            _pump = pump;
        }

        public void Start()
        {
            _subscriber.Subscribe(_pump);
        }

        public StartupPhase StartupPhaseInWhichToRun
        {
            get { return StartupPhase.IndependentInfrastructure; }
        }
    }
}