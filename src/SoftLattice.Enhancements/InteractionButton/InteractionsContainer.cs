using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SoftLattice.Common;
using System.Linq;

namespace SoftLattice.Enhancements
{
    public class InteractionsContainer
    {
        private readonly ObservableCollection<IInteractionModel> _interactionModels = new ObservableCollection<IInteractionModel>();
        private readonly ReadOnlyObservableCollection<IInteractionModel> _interactionModelsReadOnly;
        private ItemsControl _governingControl;

        public ReadOnlyObservableCollection<IInteractionModel> Interactions
        {
            get { return _interactionModelsReadOnly; }
        }
        
        public void Start(object itemsControl)
        {
            _governingControl = (ItemsControl) itemsControl;
        }

        public InteractionsContainer(IObservable<ActivateInteractionMsg> activateInteractionObservable)
        {
            _interactionModelsReadOnly = new ReadOnlyObservableCollection<IInteractionModel>(_interactionModels);

            activateInteractionObservable.Where(msg => msg.ModelInstanceAvailable)
                .ObserveOnDispatcher()
                .Subscribe(OnAttentionRequested);
        }

        private void OnAttentionRequested(ActivateInteractionMsg message)
        {
            var inBtn = message.ViewModel as IInteractionModel;
            if (inBtn == null)
                return;
            if (_interactionModels.Contains(inBtn)) //Observed under certain circumstances that the msg comes twice, by changing the DataTemplate(!) of the used model. WTF!
                return;
            inBtn.Closed += OnButtonClosed;
            _interactionModels.Add(inBtn);
        }

        private void OnButtonClosed(object interactionViewModel, EventArgs e)
        {
            var button = FindGoverningInteractionButton(interactionViewModel);
            button.TurnOff(interactionViewModel, vm => _interactionModels.Remove((IInteractionModel) vm));
        }

        private InteractionButton FindGoverningInteractionButton(object interactionViewModel)
        {
            var panel = FindStackPanel();
            var contentPresenter = panel.Children.OfType<FrameworkElement>().First(fe => fe.DataContext.Equals(interactionViewModel));
            return (InteractionButton)VisualTreeHelper.GetChild(contentPresenter, 0);
        }

        private StackPanel FindStackPanel()
        {
            DependencyObject currentItem = _governingControl;
            while (!(currentItem is StackPanel))
              currentItem = VisualTreeHelper.GetChild(currentItem, 0);
            return (StackPanel) currentItem;
        }
    }
}