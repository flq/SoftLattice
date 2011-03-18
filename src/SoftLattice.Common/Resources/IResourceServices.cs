using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;

namespace SoftLattice.Common.Resources
{
    /// <summary>
    /// Access to services related to resources
    /// </summary>
    public interface IResourceServices
    {
        /// <summary>
        /// Gives you a resourceloader instance for a given assembly
        /// </summary>
        ResourceLoader GetLoaderFor(Assembly assembly);

        /// <summary>
        /// Makes any datatemplates contained in the dictionary available to the user of this instance of <see cref="IResourceServices"/>
        /// </summary>
        void RegisterDataTemplates(ResourceDictionary dictionary);

        /// <summary>
        /// Get datatemplate for given type. Returns null if none is found
        /// </summary>
        DataTemplate this[Type dataType] { get; }

        ReadOnlyCollection<DataTemplate> DataTemplates { get; }
    }
}