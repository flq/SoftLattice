using System.Reflection;

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
    }
}