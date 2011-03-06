using System;
using Caliburn.Micro;
using MemBus;
using MemBus.Configurators;
using MemBus.Subscribing;
using SoftLattice.Common;
using SoftLattice.Core.ApplicationShell;
using SoftLattice.Core.Common;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace SoftLattice.Core.Startup
{
    internal class MainRegistry : Registry
    {
        private readonly ProgrammaticStartupInfo _info;

        public MainRegistry(ProgrammaticStartupInfo info)
        {
            _info = info;
            ForSingletonOf<IBus>().Use(constructBus);
            ForSingletonOf<IWindowManager>().Use(new WindowManager());
            ForSingletonOf<IEventAggregator>().Use(new EventAggregator());
            ForSingletonOf<KnownPluginDescriptors>().Use<KnownPluginDescriptors>();
            ForSingletonOf<ViewActivationPump>().Use<ViewActivationPump>();
            ForSingletonOf<IResourceServices>().Use<ResourceServices>();

            For(typeof(IObservable<>)).Use(typeof(MessageObservable<>));
            
            this.Delegate<IPublisher,IBus>();
            this.Delegate<ISubscriber, IBus>();

            ScanForHandlers();
            ScanForLatticeGroups();
            ScanForStartups();
        }

        private void ScanForHandlers()
        {
            ScanOverRelevantAssemblies(s => s.AddAllTypesOf(typeof(IHandles<>)));
        }

        private void ScanForStartups()
        {
            ScanOverRelevantAssemblies(s => s.AddAllTypesOf<IStartup>());
        }

        private void ScanForLatticeGroups()
        {
            ScanOverRelevantAssemblies(s => s.Convention<LatticeGroupRegistrationConvention>());
        }

        private void ScanOverRelevantAssemblies(Action<IAssemblyScanner> scannerAction)
        {
            Scan(s =>
                     {
                         s.LatticeAssembliesAndStartupInfo(_info);
                         scannerAction(s);
                     });
        }

        private static IBus constructBus()
        {
            return BusSetup.StartWith<AsyncRichClientFrontend>(
                new IoCSupport(new StructuremapBridge(() => ObjectFactory.Container)))
                .Apply<FlexibleSubscribeAdapter>(c=>c.ByMethodName("Handle").ByInterface(typeof(IHandles<>)))
                .Construct();
        }
    }
}