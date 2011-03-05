using MemBus;
using System.Linq;

namespace SoftLattice.Common.MessageStreams
{
    public class StreamOfActivatedViewModels : RxEnabledObservable<ActivateViewModelMsg>
    {
        public StreamOfActivatedViewModels(IBus bus) : base(bus)
        {
            
        }

        protected override System.IObservable<ActivateViewModelMsg> constructObservable(System.IObservable<ActivateViewModelMsg> startingPoint)
        {
            return startingPoint.Where(msg => msg.ModelInstanceAvailable).ObserveOnDispatcher();
        }
    }
}