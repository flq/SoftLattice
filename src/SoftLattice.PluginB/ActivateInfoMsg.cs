using System;
using SoftLattice.Common;

namespace SoftLattice.PluginB
{
    public class ActivateInfoMsg : ActivateInteractionMsg
    {
        public ActivateInfoMsg() : base(typeof(InfoViewModel), InteractionKind.Info)
        {
            
        }

        public string Message { get; set; }
    }
}