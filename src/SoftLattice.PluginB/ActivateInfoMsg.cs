using System;
using SoftLattice.Common;

namespace SoftLattice.PluginB
{
    public class ActivateInfoMsg : ActivateInteractionMsg
    {
        public ActivateInfoMsg() : base(typeof(InfoViewModel), InteractionKind.Information)
        {
            
        }

        public string Message { get; set; }
    }
}