using System.Reflection;

namespace SoftLattice.Core.Common
{
    internal static class UsefulExtensions
    {
        public static bool IsSoftLatticeAssembly(this Assembly assembly)
        {
            return assembly.FullName.Contains("SoftLattice");
        }

        public static bool IsSoftLatticeAssembly(this string assemblyFileName)
        {
            return assemblyFileName.Contains("SoftLattice");
        }
    }
}