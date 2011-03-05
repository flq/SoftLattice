namespace SoftLattice.Common
{
    /// <summary>
    /// Inject this into one of your classes to publish a message on the application bus.
    /// </summary>
    public interface IPublishMessage
    {
        void Publish(object message);
    }
}