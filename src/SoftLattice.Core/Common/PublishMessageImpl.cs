using System;
using MemBus;
using SoftLattice.Common;

namespace SoftLattice.Core.Common
{
    internal class PublishMessageImpl : IPublishMessage
    {
        private readonly IPublisher _publisher;

        public PublishMessageImpl(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Publish(object message)
        {
            _publisher.Publish(message);
        }
    }
}