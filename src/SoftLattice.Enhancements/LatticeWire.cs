using SoftLattice.Common;
using SoftLattice.Enhancements.Activity;

namespace SoftLattice.Enhancements
{
    public class LatticeWire : ILatticeGroup
    {
        public void Access(ILatticeWiring w)
        {
            w.AddResources(s=>s.Contains("resources"));
            w.RegisterFloatingViewModel<InteractionsContainer>("InteractionsContainer");
            w.RegisterFloatingViewModel<ActivityContainer>("ActivityContainer");
        }
    }
}