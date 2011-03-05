using System;

namespace SoftLattice.Core.Common
{
    public class PluginDescriptor
    {
        public Type TypeOfStartupView { get; private set; }

        public void AddStartupView<T>()
        {
            TypeOfStartupView = typeof (T);
        }
    }
}