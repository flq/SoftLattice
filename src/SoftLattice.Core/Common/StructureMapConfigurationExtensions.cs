using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace SoftLattice.Core.Common
{
    internal static class StructureMapConfigurationExtensions
    {
        public static void ScanLatticePluginAssemblies(this IAssemblyScanner scanner)
        {
            scanner.AssembliesFromApplicationBaseDirectory(a => a.IsSoftLatticeAssembly());
        }

        public static void AssembliesFromStartupInfo(this IAssemblyScanner scanner, ProgrammaticStartupInfo info)
        {
            if (info != null)
                foreach (var a in info.PluginAssemblies)
                    scanner.Assembly(a);
        }

        public static void LatticeAssembliesAndStartupInfo(this IAssemblyScanner scanner, ProgrammaticStartupInfo info)
        {
            scanner.ScanLatticePluginAssemblies();
            scanner.AssembliesFromStartupInfo(info);
        }

        public static void Delegate<FROM,TO>(this Registry registry) where TO : FROM
        {
            registry.For<FROM>().Use(ctx => ctx.GetInstance<TO>());
        }
    }
}