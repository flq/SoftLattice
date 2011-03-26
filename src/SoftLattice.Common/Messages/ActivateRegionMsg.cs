using System;

namespace SoftLattice.Common
{
    /// <summary>
    /// This message is to activate a specific region on a Viewmodel instance.
    /// An implementation is available thourhg the TemplatingConductor in SoftLattice.Enhacements
    /// </summary>
    [Message]
    public class ActivateRegionMsg : ActivateViewModelMsg
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="regionName">The name of the region, specified by x:Name ad nd Property name of your Principal ViewModel</param>
        /// <param name="templatingInstance">an identifier associated with the viewmodel that contains the regions you want to maintain as adressable parts of your UI</param>
        /// <param name="viewModelType">The viewmodel to be instantiated in said region</param>
        public ActivateRegionMsg(string regionName, string templatingInstance, Type viewModelType) : base(viewModelType)
        {
            RegionName = regionName;
            TemplatingInstance = templatingInstance;
        }

        public string RegionName { get; private set; }

        public string TemplatingInstance { get; private set; }
    }
}