using System;
using System.Collections.Generic;
using System.Linq;
using MemBus;
using StructureMap;

namespace SoftLattice.Core.Startup
{
    public class StructuremapBridge : IocAdapter
    {
        private readonly Func<IContainer> containerLookup;

        private IContainer container
        {
            get { return containerLookup(); }
        }

        public StructuremapBridge(Func<IContainer> containerLookup)
        {
            this.containerLookup = containerLookup;
        }


        public IEnumerable<object> GetAllInstances(Type desiredType)
        {
            return container.GetAllInstances(desiredType).OfType<object>();
        }
    }
}