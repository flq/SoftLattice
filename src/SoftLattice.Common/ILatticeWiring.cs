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
        /// Much like you providing an instance, this overload passes the responsibility to the container
        /// to instantiate the type and then register it with the bus.
        /// </summary>
        void RegisterMessageListener<T>();

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

        /// <summary>
        /// This functionality is related to the Bind.Model attached property of Caliburn.Micro. 
        /// Register a ViewModel with SoftLattice that will be instantiated when the provided key 
        /// is used with said attached property.
        /// </summary>
        void RegisterFloatingViewModel<ViewModel>(string modelKey);

        /// <summary>
        /// Register a singleton service in the system. If the service has been defined beforehand, the new definition will overwrite it
        /// </summary>
        void RegisterSingleService<SVC, IMPL>() where IMPL : SVC;

        /// <summary>
        /// Register a type whose deserialized form is contained in the application configuration file. This plays well with the 
        /// ConfigReader contained in SoftLattice.Enhancements. Everytime you state a dependency to this object, it will be obtained
        /// by deserializing the XmlSerialized form of the object. Note that a valid implementation of <see cref="IAppConfiguration"/>
        /// is necessary. If you use SoftLattice.Enhancements, you will auotmatically have one that accesses the application config
        /// </summary>
        void GetFromAppConfig<T>();
    }
}