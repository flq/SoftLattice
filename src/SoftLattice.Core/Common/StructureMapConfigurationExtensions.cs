using StructureMap.Graph;

namespace SoftLattice.Core.Common
{
    public static class StructureMapConfigurationExtensions
    {
        public static void ScanLatticePluginAssemblies(this IAssemblyScanner scanner)
        {
            scanner.AssembliesFromApplicationBaseDirectory(a => a.FullName.Contains("LatticeGroup"));
        }
    }
}