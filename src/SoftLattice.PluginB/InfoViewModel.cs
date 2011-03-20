using System;
using SoftLattice.Common;

namespace SoftLattice.PluginB
{
    public class InfoViewModel : IInteractionModel
    {
        public InfoViewModel(ActivateViewModelMsg activateInfoMsg)
        {
            Message = ((ActivateInfoMsg)activateInfoMsg).Message;
        }

        public string Message { get; private set; }
        public void Clicked()
        {
            if (Closed != null)
                Closed(this, EventArgs.Empty);
        }

        public event EventHandler Closed;
    }
}