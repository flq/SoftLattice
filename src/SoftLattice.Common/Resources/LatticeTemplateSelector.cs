using System.Windows.Controls;

namespace SoftLattice.Common.Resources
{
    /// <summary>
    /// This is a special template selector that lives off the SoftLattice infrastructure
    /// and will have access to all DataTemplates registered by the system
    /// </summary>
    public class LatticeTemplateSelector : DataTemplateSelector
    {
        private static IResourceServices _services;

        /// <summary>
        /// Since WPF instantiates this class when used in XAML, this method is used in SoftLattice startup to associate the
        /// <see cref="IResourceServices"/> instance with the selector
        /// </summary>
        /// <param name="services"></param>
        public static void SetResourceServices(IResourceServices services)
        {
            _services = services;
        }

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            // No idea why resharper thinks it cannot be null
            if (item != null && _services != null)
            {
                var dt = _services[item.GetType()];
                if (dt != null)
                    return dt;
            }
            return base.SelectTemplate(item, container);
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }
    }
}