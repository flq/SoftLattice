using SoftLattice.Common;
using SoftLattice.Enhancements.Messages;

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
            _publisher.Publish(new ShowTextToUserMsg("This is an error", InteractionKind.Error));
        }

        public void StartWarning()
        {
            _publisher.Publish(new ShowTextToUserMsg("This is a warning", InteractionKind.Warning));
        }

        public void StartInfo()
        {
            _publisher.Publish(new ShowTextToUserMsg("This is an info!", InteractionKind.Info));
        }

        public void StartActivity()
        {
            _publisher.Publish(new ActivityMsg("Ho HO HO!"));
        }

        public void EndActivity()
        {
            _publisher.Publish(new ActivityMsg());
        }
    }
}