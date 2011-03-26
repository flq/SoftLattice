using System;
using System.Windows.Threading;

namespace SoftLattice.Core.Common
{
    public interface IDispatchServices
    {
        Dispatcher Dispatcher { get; }
        void EnsureActionOnDispatcher(Action action);
    }
}