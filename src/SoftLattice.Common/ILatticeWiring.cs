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
    }
}