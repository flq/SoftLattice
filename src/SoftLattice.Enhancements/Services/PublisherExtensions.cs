using SoftLattice.Common;
using SoftLattice.Enhancements.Messages;

namespace SoftLattice.Enhancements.Services
{
    public static class PublisherExtensions
    {
        public static void ShowTextToUser(this IPublishMessage publisher, string message)
        {
            publisher.Publish(new ShowTextToUserMsg(message, InteractionKind.Info));
        }
    }
}