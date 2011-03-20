using SoftLattice.Common;

namespace SoftLattice.PluginA
{
    public class Contributor : ILatticeGroup
    {
        private readonly IPublishMessage _publish;

        public Contributor(IPublishMessage publish)
        {
            _publish = publish;
        }

        public void Access(ILatticeWiring wiring)
        {
            wiring.RegisterMessageListener(MessageListener.Instance);
            wiring.RegisterMessageListener(this);
            wiring.AddResource("Plugin.xaml");
            wiring.AddResources(path=>path.StartsWith("lookupresources"));
        }

        public void Handle(StartupMsg msg)
        {
            _publish.Publish(new ActivatePluginMsg(typeof(PluginAEntryViewModel), "The Plugin A"));
        }
    }
}