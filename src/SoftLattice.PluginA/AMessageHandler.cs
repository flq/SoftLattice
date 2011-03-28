using SoftLattice.Common;

namespace SoftLattice.PluginA
{
    public class AMessageHandler
    {
        private readonly IPublishMessage _publisher;

        public AMessageHandler(IPublishMessage publisher)
        {
            _publisher = publisher;
        }

        public void Handle(StartupMsg msg)
        {
            _publisher.Publish(new MarkerMessageForFloatingListenerMsg());
        }
    }

    public class MarkerMessageForFloatingListenerMsg
    {
    }
}