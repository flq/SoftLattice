using MemBus;
using MemBus.Configurators;
using MemBus.Subscribing;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace SoftLattice.Core.Startup
{
    public class MainRegistry : Registry
    {
        public MainRegistry()
        {
            ForSingletonOf<IBus>().Use(constructBus);
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