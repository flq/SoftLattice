using System;
using MemBus.Publishing;

namespace SoftLattice.Core.Common
{
    public class DeferredPublishPipelineMember<T> : IPublishPipelineMember where T : IPublishPipelineMember
    {
        private readonly Func<T> _actualPipelineMember;

        public DeferredPublishPipelineMember(Func<T> actualPipelineMember)
        {
            _actualPipelineMember = actualPipelineMember;
        }

        public void LookAt(PublishToken token)
        {
            var member = _actualPipelineMember();
            member.LookAt(token);
        }
    }
}