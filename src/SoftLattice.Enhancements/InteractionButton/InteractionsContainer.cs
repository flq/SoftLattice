using System.Collections.ObjectModel;

namespace SoftLattice.Enhancements
{
    public class InteractionsContainer
    {
        private readonly ObservableCollection<InteractionButtonModel> _interactionModels = new ObservableCollection<InteractionButtonModel>();
        private readonly ReadOnlyObservableCollection<InteractionButtonModel> _interactionModelsReadOnly; //TODO: instantiate in constructor

        public ReadOnlyObservableCollection<InteractionButtonModel> Interactions
        {
            get { return _interactionModelsReadOnly; }
        }
        

        public InteractionsContainer()
        {
            _interactionModelsReadOnly = new ReadOnlyObservableCollection<InteractionButtonModel>(_interactionModels);
        }


    }
}