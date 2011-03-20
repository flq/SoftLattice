using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using MemBus;
using SoftLattice.Common;
using SoftLattice.Core.ApplicationShell;
using SoftLattice.Core.Common;
using StructureMap;

namespace SoftLattice.Core.Startup
{
    public class AppBootstrap : Bootstrapper<ShellViewModel>
    {
        private IContainer container;
        private readonly AssemblyLoader _assemblyLoader = new AssemblyLoader();

        protected override void Configure()
        {
            var info = (ProgrammaticStartupInfo)Application.Current.Properties["override"];
            ObjectFactory.Initialize(i => i.AddRegistry(new MainRegistry(info)));
            container = ObjectFactory.Container;
        }

        protected override object GetInstance(Type service, string key)
        {
            if (service == null)
                service = typeof (object);
            var returnValue = container.GetInstance(service, key);
            return returnValue;
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service).OfType<object>();
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return _assemblyLoader.Assemblies;
        }

        protected override void DisplayRootView()
        {
            var runner = container.GetInstance<StartupRunner>();
            runner.Run(StartupPhase.IndependentInfrastructure, StartupPhase.AllPluginsKnown);
            var shell = container.GetInstance<ShellViewModel>();
            container.GetInstance<IWindowManager>().ShowWindow(shell);
            var bus = container.GetInstance<IBus>();
            bus.Publish(new StartupMsg());
            runner.Run(StartupPhase.UIShown);
        }
    }
}