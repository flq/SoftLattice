using MemBus;
using SoftLattice.Common;
using StructureMap;

namespace SoftLattice.Core.ApplicationShell
{
    public class ViewActivationPump
    {
        private readonly IPublisher _publisher;
        private readonly IContainer _container;

        public ViewActivationPump(IPublisher publisher, IContainer container)
        {
            _publisher = publisher;
            _container = container;
        }

        public void Handle(ActivateViewModelMsg message)
        {
            if (message.ModelInstanceAvailable) // Already instantiated
                return;

            var obj = _container.GetInstance(message.ViewModelType);

            if (obj == null) // SM could not instantiate the type for whatever reason
                return;

            message.SetInstantiatedModel(obj);
            _publisher.Publish(message);
        }
    }
}