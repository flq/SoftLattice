using MemBus;
using StructureMap;

namespace SoftLattice.Core.Common
{
    /// <summary>
    /// For use by integration tests
    /// </summary>
    internal class MessageSender
    {
        public static void SendMessage(object message)
        {
            var bus = ObjectFactory.Container.GetInstance<IBus>();
            bus.Publish(message);
        }
    }
}