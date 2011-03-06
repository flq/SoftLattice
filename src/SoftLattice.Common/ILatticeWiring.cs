namespace SoftLattice.Common
{
    /// <summary>
    /// Access to the Software Lattice wiring
    /// </summary>
    public interface ILatticeWiring
    {
        /// <summary>
        /// SoftLattice will look for public methods on the passed in instance named Handle that have a void return type and accept
        /// a single object as argument. Whenever a message of that type is published on the application bus, the corresponding method
        /// will be called.
        /// </summary>
        void RegisterMessageListener(object listener);

        /// <summary>
        /// If your group has a view to be shown on stratup (The start screen if yo wish), specify the corresponding viewmodel
        /// through this method
        /// </summary>
        void RegisterStartupView<T>();

        /// <summary>
        /// Allows your plugin to add resources to the SoftLattice runtime. Please consider that SoftLattice may be running
        /// several plugins, hence put yourself in the habit of prefixing your resource keys with some identifier related to your plugin
        /// to avoid naming collisions.
        /// </summary>
        void AddResource(string relativePath);
    }
}