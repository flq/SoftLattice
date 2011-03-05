using System;
using SoftLattice.Common;
using SoftLattice.Core.Common;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace SoftLattice.Core.Startup
{
    public class LatticeGroupRegistrationConvention : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if (type.ImplementsInterface<ILatticeGroup>() && !type.IsAbstract && type.IsPublic)
            {
                registry.AddType(typeof(ILatticeGroup), type, type.FullName);
            }
        }
    }
}