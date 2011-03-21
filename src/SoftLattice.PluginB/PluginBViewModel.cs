using SoftLattice.Common;

namespace SoftLattice.PluginB
{
    public class PluginBViewModel
    {
        private readonly IPublishMessage _publisher;

        public PluginBViewModel(IPublishMessage publisher)
        {
            _publisher = publisher;
        }

        public void StartError()
        {
            _publisher.Publish(new ActivateInteractionMsg(typeof(InfoViewModel), InteractionKind.Error));
        }

        public void StartWarning()
        {
            _publisher.Publish(new ActivateInteractionMsg(typeof(InfoViewModel), InteractionKind.Warning));
        }

        public void StartInfo()
        {
            _publisher.Publish(new ActivateInfoMsg { Message = "Hello World!"});
        }
    }
}