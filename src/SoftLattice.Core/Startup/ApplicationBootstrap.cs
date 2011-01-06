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

        protected override void Configure()
        {
            var info = (ProgrammaticStartupInfo)Application.Current.Properties["override"];
            ObjectFactory.Initialize(i => {
                i.AddRegistry<MainRegistry>();
                i.Scan(s =>
                           {
                               s.AssembliesFromApplicationBaseDirectory(a => a.FullName.Contains("SnarkyPlugin"));
                               if (info != null)
                                   foreach (var a in info.PluginAssemblies)
                                       s.Assembly(a);
                               s.Convention<SnarkyPluginScan>();
                           });
            });
            ObjectFactory.Configure(c =>
            {
                c.ForSingletonOf<IWindowManager>().Use(new WindowManager());
                c.ForSingletonOf<IEventAggregator>().Use(new EventAggregator());
            });
            container = ObjectFactory.Container;
            //wireUpPlugins();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
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
            return new[] { Assembly.GetExecutingAssembly() };
        }

        protected override void DisplayRootView()
        {
            wireUpPlugins();
            var shell = container.GetInstance<ShellViewModel>();
            container.GetInstance<IWindowManager>().Show(shell);
            var bus = container.GetInstance<IBus>();
            bus.Publish(new StartupMsg());
        }

        private void wireUpPlugins()
        {
            var contribs = container.GetAllInstances<ILatticeGroup>();
            var wiring = container.GetInstance<LatticeWiring>();
            foreach (var c in contribs)
                c.Access(wiring);
        }
    }
}