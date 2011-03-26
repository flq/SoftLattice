using SoftLattice.Common;
using SoftLattice.Common.Resources;

namespace SoftLattice.Core.Startup
{
    /// <summary>
    /// -Associate the WPF-instantiated Templateselector to access the DataTemplates known to SoftLattice
    /// </summary>
    public class WireUpInfrastructure : IStartup
    {
        public WireUpInfrastructure(IResourceServices services)
        {
            LatticeTemplateSelector.SetResourceServices(services);
        }

        public void Start()
        {
        }

        public StartupPhase StartupPhaseInWhichToRun
        {
            get { return StartupPhase.IndependentInfrastructure; }
        }
    }
}