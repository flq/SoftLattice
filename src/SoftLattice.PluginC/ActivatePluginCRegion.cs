using System;
using SoftLattice.Common;

namespace SoftLattice.PluginC
{
    public class ActivatePluginCRegion : ActivateRegionMsg
    {
        public ActivatePluginCRegion(string regionName, Type viewModelType) : base(regionName, "PluginC", viewModelType)
        {
        }
    }
}