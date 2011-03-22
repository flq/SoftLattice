using SoftLattice.Common;
using SoftLattice.Enhancements.Models;

namespace SoftLattice.Enhancements.Messages
{
    public class ShowTextToUserMsg : ActivateInteractionMsg
    {
        public ShowTextToUserMsg(string message, InteractionKind interactionKind)
            : base(typeof(MessageToUserViewModel), interactionKind)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}