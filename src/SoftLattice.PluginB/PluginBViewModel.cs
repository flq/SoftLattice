using SoftLattice.Common;
using SoftLattice.Enhancements.Messages;
using SoftLattice.Enhancements.Services;

namespace SoftLattice.PluginB
{
    public class PluginBViewModel
    {
        private readonly IPublishMessage _publisher;
        private readonly IApplicationStorage _storage;

        public PluginBViewModel(IPublishMessage publisher, IApplicationStorage storage)
        {
            _publisher = publisher;
            _storage = storage;
            Value = _storage.Get<string>("PluginB.Value");
        }

        public string Value { get; set; }

        public void StoreValue()
        {
            _storage.Put("PluginB.Value", Value);
            _publisher.ShowTextToUser("Value saved");
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
            _publisher.ShowTextToUser("This is an info!");
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