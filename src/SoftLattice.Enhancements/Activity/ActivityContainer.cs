using System;
using System.Linq;
using MemBus.Support;
using SoftLattice.Common;

namespace SoftLattice.Enhancements.Activity
{
    public class ActivityContainer : Caliburn.Micro.PropertyChangedBase
    {
        private readonly DisposeContainer disposer = new DisposeContainer();

        public ActivityContainer(IObservable<ActivityMsg> activityMessages)
        {
            var busyStream1 = activityMessages
               .Where(msg => msg.GettingBusy)
               .ObserveOnDispatcher().Subscribe(OnGettingBusy);
            var busyStream2 = activityMessages
                .Where(msg => msg.GettingCalm)
                .ObserveOnDispatcher().Subscribe(OnGettingCalm);
            disposer.Add(busyStream1, busyStream2);
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (value.Equals(isBusy))
                    return;
                isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
            }
        }

        private string busyMessage;
        public string BusyMessage
        {
            get { return busyMessage; }
            set
            {
                if (value == null || value.Equals(busyMessage))
                    return;
                busyMessage = value;
                NotifyOfPropertyChange(() => BusyMessage);
            }
        }

        private void OnGettingBusy(ActivityMsg msg)
        {
            BusyMessage = string.Empty;
            IsBusy = true;
            BusyMessage = msg.BusyText;
        }

        private void OnGettingCalm(ActivityMsg msg)
        {
            IsBusy = false;
        }
    }
}