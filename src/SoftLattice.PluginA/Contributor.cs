using SoftLattice.Common;

namespace SoftLattice.PluginA
{
    public class Contributor : ILatticeGroup
    {
        public void Access(ILatticeWiring wiring)
        {
            wiring.RegisterMessageListener(MessageListener.Instance);
            wiring.RegisterStartupView<PluginAEntryViewModel>();
        }
    }
}