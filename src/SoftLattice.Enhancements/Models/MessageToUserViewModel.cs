using System;
using SoftLattice.Common;
using SoftLattice.Enhancements.Messages;

namespace SoftLattice.Enhancements.Models
{
    public class MessageToUserViewModel : IInteractionModel
    {
        public MessageToUserViewModel(ActivateViewModelMsg activateInfoMsg)
        {
            var msg = activateInfoMsg as ShowTextToUserMsg;
            Message = msg != null ? msg.Message : "No Message";
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