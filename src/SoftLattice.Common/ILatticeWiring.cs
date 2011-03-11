using System;

namespace SoftLattice.Common
{
    /// <summary>
    /// Access to the Software Lattice wiring
    /// </summary>
    public interface ILatticeWiring
    {
        /// <summary>
        /// SoftLattice will look for public methods on the passed in instance named "Handle" that have a void return type and accept
        /// a single object as argument. Whenever a message of that type is published on the application bus, the corresponding method
        /// will be called. An example:
        /// * SoftLattice sends the <see cref="StartupMsg"/>, you as plugin send the <see cref="ActivatePluginMsg"/> 
        /// </summary>
        void RegisterMessageListener(object listener);

        /// <summary>
        /// Allows your plugin to add resources to the SoftLattice runtime. Please consider that SoftLattice may be running
        /// several plugins, hence put yourself in the habit of prefixing your resource keys with some identifier related to your plugin
        /// to avoid naming collisions.
        /// </summary>
        void AddResource(string relativePath);

        /// <summary>
        /// Provide a function that decides upon a passed in path to resources in your assembly whether you want it included or not.
        /// All resources compiled as page are found in your pluginassembly and the corresponding file passed in to the predicate
        /// The path is not identical to the filesystem path:
        /// * It is all lowercase
        /// * The .xaml extension is removed
        /// </summary>
        void AddResources(Func<string, bool> pathPredicate);
    }
}