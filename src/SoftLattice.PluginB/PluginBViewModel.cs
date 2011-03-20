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
            //_publisher.Publish();
        }

        public void StartWarning()
        {

        }

        public void StartInfo()
        {
            _publisher.Publish(new ActivateInfoMsg { Message = "Hello World!"});
        }
    }
}