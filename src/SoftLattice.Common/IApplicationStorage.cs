namespace SoftLattice.Common
{
    /// <summary>
    /// Basic interface for the application store. The SoftLattice.Enhancements provides a basic implementation that XML-serializes stored values to a file
    /// in the executable directory
    /// </summary>
    public interface IApplicationStorage
    {
        /// <summary>
        /// Put in an object under a given key
        /// </summary>
        void Put(string key, object value);

        /// <summary>
        /// Get an object for a given key
        /// </summary>
        T Get<T>(string key);
    }
}