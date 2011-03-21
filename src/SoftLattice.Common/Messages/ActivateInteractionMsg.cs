using System;

namespace SoftLattice.Common
{
    /// <summary>
    /// The kind of onteraction that is requested
    /// </summary>
    public enum InteractionKind
    {
        Info,
        Warning,
        Error
    }

    /// <summary>
    /// This message should be sent when some typical "MessageBox" style interaction is necessary with the user.
    /// View activation messages are handled synchronously, i.e. you can get blocking effects and read values off
    /// the message after execution returns from the "Publish" call.
    /// </summary>
    [Message]
    public class ActivateInteractionMsg : ActivateViewModelMsg
    {
        private readonly InteractionKind _interactionKind;

        public ActivateInteractionMsg(Type interactionViewModelType, InteractionKind interactionKind) : base(interactionViewModelType)
        {
            _interactionKind = interactionKind;
        }

        public InteractionKind InteractionKind
        {
            get { return _interactionKind; }
        }
    }
}