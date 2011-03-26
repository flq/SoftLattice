using MemBus.Publishing;
using SoftLattice.Common;
using SoftLattice.Core.Common;
using StructureMap;

namespace SoftLattice.Core.ApplicationShell
{
    public class ViewActivationPump : IPublishPipelineMember
    {
        private readonly IContainer _container;
        private readonly IDispatchServices _dispatchServices;

        public ViewActivationPump(IContainer container, IDispatchServices dispatchServices)
        {
            _container = container;
            _dispatchServices = dispatchServices;
        }

        public void LookAt(PublishToken token)
        {
            var message = (ActivateViewModelMsg)token.Message;

            // Sometimes objects that are bound to WPF and are created not on the dispatcher thread throw exceptions
            // hence we ensure that viewmodels are created on said dispatcher thread
            _dispatchServices.EnsureActionOnDispatcher(
                () =>
                    {
                        var obj = _container.With(message).GetInstance(message.ViewModelType);

                        if (obj == null)
                            // SM could not instantiate the type for whatever reason
                            return;
                        message.SetInstantiatedModel(obj);
                    });
        }
    }
}