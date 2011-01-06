using MemBus;
using SoftLattice.Common;

namespace SoftLattice.Core.Startup
{
    public class LatticeWiring : ILatticeWiring
    {
        private readonly IBus _bus;

        public LatticeWiring(IBus bus)
        {
            _bus = bus;
        }

        void ILatticeWiring.RegisterMessageListener(object listener)
        {
            _bus.Subscribe(listener);
        }
    }
}