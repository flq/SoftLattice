using NUnit.Framework;
using SoftLattice.Common;
using SoftLattice.Tests.Frame;

namespace SoftLattice.Tests.Integration.UI
{
    [TestFixture,Ignore]
    public class app_run_context
    {
        protected AppRunner appRunner;

        [TestFixtureSetUp]
        public void given()
        {
            appRunner = new AppRunner();
            appRunner.Run();
        }

        [TestFixtureTearDown]
        public void end()
        {
            appRunner.SendMessageToApp(new ShutdownMsg());
        }
    }
}