using NUnit.Framework;
using SoftLattice.Common;
using SoftLattice.Tests.Frame;

namespace SoftLattice.Tests.Integration.UI
{
    public class when_app_starts : app_run_context
    {
        [Test]
        public void startup_message_is_received()
        {
            TestMessageListener.Instance.Contains<StartupMsg>().ShouldBeTrue();
        }
    }
}