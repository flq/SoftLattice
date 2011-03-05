using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using MemBus;
using MemBus.Support;
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

        public AppBootstrap()
        {
          ViewLocator.LocateTypeForModelType  = (modelType, displayLocation, context) => {
            var viewTypeName = modelType.FullName.Substring(0, modelType.FullName.IndexOf("`") < 0
                ? modelType.FullName.Length
                : modelType.FullName.IndexOf("`")
                ).Replace("Model", string.Empty);

            if (context != null)
            {
                viewTypeName = viewTypeName.Remove(viewTypeName.Length - 4, 4);
                viewTypeName = viewTypeName + "." + context;
            }

            var viewType = (from assmebly in AssemblySource.Instance
                            from type in assmebly.GetExportedTypes()
                            where type.FullName == viewTypeName
                            select type).FirstOrDefault();

            return viewType;
        };
        }

        protected override void Configure()
        {
            var info = (ProgrammaticStartupInfo)Application.Current.Properties["override"];
            ObjectFactory.Initialize(i => i.AddRegistry(new MainRegistry(info)));
            container = ObjectFactory.Container;
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