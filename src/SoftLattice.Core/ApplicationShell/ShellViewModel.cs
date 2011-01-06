using Caliburn.Micro;
using MemBus;
using SoftLattice.Common;

namespace SoftLattice.Core.ApplicationShell
{
    public class ShellViewModel : Conductor<Screen>
    {
        private readonly IBus _bus;

        public ShellViewModel(IBus bus)
        {
            _bus = bus;
            _bus.Subscribe<ShutdownMsg>(onShutDown);
        }

        void onShutDown(ShutdownMsg msg)
        {
            base.OnDeactivate(true);
        }
    }
}