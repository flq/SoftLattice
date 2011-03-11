using System;
using System.Windows.Automation;
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

        [Test]
        public void the_resource_provided_by_name_is_added_to_the_system()
        {
            var text = GetTextFromElement("StyledTextbox");
            text.ShouldNotBeNull();
            text.ShouldBeEqualTo("FromResource");
        }

        [Test]
        public void the_resource_from_lookup_is_correctly_loaded()
        {
            var text = GetTextFromElement("StyledTextboxFromLookup");
            text.ShouldNotBeNull();
            text.ShouldBeEqualTo("FromLookupResource");
        }


    }
}