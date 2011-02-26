using Caliburn.Micro;
using MemBus;
using MemBus.Configurators;
using MemBus.Subscribing;
using SoftLattice.Core.Common;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace SoftLattice.Core.Startup
{
    internal class MainRegistry : Registry
    {
        public MainRegistry(ProgrammaticStartupInfo info)
        {
            ForSingletonOf<IBus>().Use(constructBus);
            Scan(s =>
            {
                s.ScanLatticePluginAssemblies();
                if (info != null)
                    foreach (var a in info.PluginAssemblies)
                        s.Assembly(a);
                s.Convention<LatticeGroupScan>();
            });
            ForSingletonOf<IWindowManager>().Use(new WindowManager());
            ForSingletonOf<IEventAggregator>().Use(new EventAggregator());
        }

        private static IBus constructBus()
        {
            return BusSetup.StartWith<AsyncRichClientFrontend>(
                new IoCSupport(new StructuremapBridge(() => ObjectFactory.Container)))
                .Apply<FlexibleSubscribeAdapter>(c=>c.ByMethodName("Handle"))
                .Construct();
        }
    }
}