using SoftLattice.Common;
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

        public static void LatticeAssemblies(this IAssemblyScanner scanner, ProgrammaticStartupInfo info)
        {
            if (info != null)
            {
                scanner.AssembliesFromStartupInfo(info);
                scanner.AssemblyContainingType<StartupRunner>();
                scanner.AssemblyContainingType<ILatticeGroup>();
            }
            else
            {
                scanner.ScanLatticePluginAssemblies();
            }
        }

        public static void Delegate<FROM,TO>(this Registry registry) where TO : FROM
        {
            registry.For<FROM>().Use(ctx => ctx.GetInstance<TO>());
        }
    }
}