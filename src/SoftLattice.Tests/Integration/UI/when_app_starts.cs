using System;
using NUnit.Framework;
using SoftLattice.Common;
using SoftLattice.PluginA;
using SoftLattice.Tests.Frame;

namespace SoftLattice.Tests.Integration.UI
{
    public class when_app_starts : app_run_context
    {
        public when_app_starts()
        {
            LoadWithPlugins("PluginA");
        }

        [Test]
        public void startup_message_is_received()
        {
            MessageListener.Instance.Contains<StartupMsg>().ShouldBeTrue();
        }

        [Test]
        public void the_designated_viewmodel_is_loaded_into_the_window()
        {
            var start = FindByName("PluginAEntryView");
            start.ShouldNotBeNull();
        }
    }
}