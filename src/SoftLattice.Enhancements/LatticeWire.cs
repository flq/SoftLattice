using SoftLattice.Common;

namespace SoftLattice.Enhancements
{
    public class LatticeWire : ILatticeGroup
    {
        public void Access(ILatticeWiring wiring)
        {
            wiring.AddResources(s=>s.Contains("resources"));
        }
    }
}