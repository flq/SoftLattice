using StructureMap.Configuration.DSL;

namespace SoftLattice.PluginA
{
    public class RegisterStuff : Registry
    {
        public RegisterStuff()
        {
            ForSingletonOf<IDependency>().Use<Dependency>();
        }
    }
}