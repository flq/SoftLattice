using System;
using Caliburn.Micro;
using MemBus;
using MemBus.Support;
using SoftLattice.Common;
using System.Linq;

namespace SoftLattice.Core.ApplicationShell
{
    public class ShellViewModel : Conductor<object>
    {
        private readonly IBus _bus;
        private readonly DisposeContainer _disposer = new DisposeContainer();

        public ShellViewModel(IBus bus)
        {
            _bus = bus;
            _disposer.Add(setupObserver());
            _disposer.Add(_bus.Subscribe(this));
        }

        private IDisposable setupObserver()
        {
            return _bus.Observe<ActivatePluginMsg>()
                .Where(msg => msg.ModelInstanceAvailable)
                .ObserveOnDispatcher()
                .Subscribe(onViewActivationRequested);
        }

        public void Handle(ShutdownMsg msg)
        {
            base.OnDeactivate(true);
        }

        protected override void OnDeactivate(bool close)
        {
            _disposer.Dispose();
            base.OnDeactivate(close);
        }

        private void onViewActivationRequested(ActivatePluginMsg msg)
        {
            base.DisplayName = msg.PluginTitle;
            ActivateItem(msg.ViewModel);
        }
    }
}