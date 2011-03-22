namespace SoftLattice.Common
{
    [Message]
    public class ActivityMsg
    {
        public bool GettingBusy { get; private set; }
        public bool GettingCalm { get { return !GettingBusy; } }
        public string BusyText { get; private set; }

        /// <summary>
        /// Send a message that will state <see cref="GettingCalm"/>
        /// </summary>
        public ActivityMsg()
        {
            GettingBusy = false;
        }

        public ActivityMsg(string busyText)
        {
            GettingBusy = true;
            BusyText = busyText;
        }
    }
}