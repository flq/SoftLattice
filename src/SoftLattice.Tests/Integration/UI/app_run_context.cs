using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Automation;
using NUnit.Framework;
using SoftLattice.Common;
using SoftLattice.Tests.Frame;

namespace SoftLattice.Tests.Integration.UI
{
    
    public class app_run_context
    {
        protected AppRunner appRunner;

        public app_run_context()
        {
            appRunner = new AppRunner();
        }

        [TestFixtureSetUp]
        public void Run()
        {
            appRunner.Run();
        }

        [TestFixtureTearDown]
        public void EndAppRunner()
        {
            appRunner.SendMessageToApp(new ShutdownMsg());
        }

        protected void LoadWithPlugins(params string[] plugin)
        {
            var result = from p in plugin
                         let pluginDllName = "SoftLattice." + p + ".dll"
                         where File.Exists(pluginDllName)
                         select Assembly.LoadFrom(pluginDllName);
            appRunner.AddPlugins(result);
        }

        protected AutomationElement FindByName(string automationId)
        {
            return appRunner.FindByAutomationId(automationId);
        }

        protected string GetTextFromElement(string automationId)
        {
            var textPattern = appRunner.GetTextPattern(automationId);
            if (textPattern == null)
                return null;
            return textPattern.DocumentRange.GetText(-1);
        }
    }
}