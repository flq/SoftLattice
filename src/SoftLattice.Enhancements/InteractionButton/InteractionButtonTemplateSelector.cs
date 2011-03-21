using System.Collections.Concurrent;
using System.Windows;
using System.Windows.Controls;
using SoftLattice.Common;

namespace SoftLattice.Enhancements
{
    public class InteractionButtonTemplateSelector : DataTemplateSelector
    {
        private static readonly ConcurrentDictionary<int, InteractionKind> buttonTypeRequestCache = new ConcurrentDictionary<int, InteractionKind>();

        public static void RegisterRequiredInteractionKind(int dataContextHashCode, InteractionKind requestedButtonType)
        {
            buttonTypeRequestCache.AddOrUpdate(dataContextHashCode, requestedButtonType, (i,ik) => requestedButtonType);
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            InteractionKind requestedButtonType;
            var success = buttonTypeRequestCache.TryRemove(item.GetHashCode(), out requestedButtonType);
            if (success)
            {
                return tryFindResource("InteractionButton.Template" + requestedButtonType);
            }

            return base.SelectTemplate(item, container);
        }

        private static DataTemplate tryFindResource(string resourceKey)
        {
            var dataTemplate = (DataTemplate)Application.Current.TryFindResource(resourceKey);
            return dataTemplate;
        }
    }
}