using SoftLattice.Common;
using SoftLattice.Tests.Frame;

namespace SoftLattice.Tests.Plugin
{
    public class Contributor : ILatticeGroup
    {
        public void Access(ILatticeWiring wiring)
        {
            wiring.RegisterMessageListener(TestMessageListener.Instance);
        }
    }
}