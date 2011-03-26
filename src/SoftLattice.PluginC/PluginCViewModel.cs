using System;
using SoftLattice.Common;
using SoftLattice.Enhancements.Models;

namespace SoftLattice.PluginC
{
    public class PluginCViewModel : TemplatingConductor
    {
        private readonly IPublishMessage _publisher;


        public PluginCViewModel(IObservable<ActivateRegionMsg> activateRegionMessages, IPublishMessage publisher) : base(activateRegionMessages, "PluginC")
        {
            _publisher = publisher;
        }

        public object AreaA { get; private set; }
        public object AreaB { get; private set; }

        public void LoadItem1IntoAreaA()
        {
            _publisher.Publish(new ActivatePluginCRegion("AreaA", typeof(Item1ViewModel)));
        }

        public void LoadItem2IntoAreaB()
        {
            _publisher.Publish(new ActivatePluginCRegion("AreaB", typeof (Item2ViewModel)));
        }
    }
}