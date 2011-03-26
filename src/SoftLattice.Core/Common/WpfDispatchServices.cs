using System;
using System.Windows;
using System.Windows.Threading;

namespace SoftLattice.Core.Common
{
    public class WpfDispatchServices : IDispatchServices
    {
        public WpfDispatchServices(DispatcherObject app)
        {
            Dispatcher = app.Dispatcher;
        }

        public Dispatcher Dispatcher
        {
            get; private set;
        }

        public void EnsureActionOnDispatcher(Action action)
        {
            var isOnThread = Dispatcher.CheckAccess();
            if (isOnThread)
                action();
            else
                Dispatcher.Invoke(action);
        }
    }
}