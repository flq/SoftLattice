using System.ComponentModel;
using SoftLattice.Common;

namespace SoftLattice.PluginC
{
    public class PluginCViewModel : INotifyPropertyChanged
    {
        private readonly IPublishMessage _publisher;

        public PluginCViewModel(IPublishMessage publisher)
        {
            _publisher = publisher;
        }

        public object AreaA { get; set; }
        public object AreaB { get; set; }

        public void LoadItem1IntoAreaA()
        {
            AreaA = new Item1ViewModel();
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("AreaA"));
        }

        public void LoadItem2IntoAreaB()
        {
            AreaB = new Item2ViewModel();
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("AreaB"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}