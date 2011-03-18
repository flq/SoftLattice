using System.Windows;
using System.Windows.Controls;

namespace SoftLattice.Enhancements
{
    public class InteractionButtonTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null) // No, it's not always false, resharper! if there is no data context, it is null
                return base.SelectTemplate(item, container);
            return tryFindResource(item.GetType().Name);
        }

        private static DataTemplate tryFindResource(string resourceKey)
        {
            var dataTemplate = (DataTemplate)Application.Current.TryFindResource(resourceKey);
            return dataTemplate;
        }
    }
}