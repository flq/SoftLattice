using SoftLattice.Common;
using SoftLattice.Tests.Frame;

namespace SoftLattice.Tests.PluginA
{
    public class Contributor : ILatticeGroup
    {
        public void Access(ILatticeWiring wiring)
        {
            wiring.RegisterMessageListener(TestMessageListener.Instance);
            wiring.RegisterStartupView<PluginAEntryViewModel>();
        }
    }
}