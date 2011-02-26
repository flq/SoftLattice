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
            var compiler = new PluginCompiler();
            compiler
                .ReferenceThisAssembly("SoftLattice.Tests.dll")
                .ReferenceThisAssembly("SoftLattice.Common.dll")
                .With(plugin);
            appRunner.AddPlugins(compiler.Assemblies);
        }

        protected AutomationElement FindByName(string name)
        {
            return appRunner.FindById(name);
        }
    }
}