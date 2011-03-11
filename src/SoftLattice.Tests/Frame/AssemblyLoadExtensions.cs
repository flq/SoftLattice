using System.IO;
using System.Reflection;

namespace SoftLattice.Tests.Frame
{
    public static class AssemblyLoadExtensions
    {
        public static Assembly Load(this string pluginName)
        {
            var pluginDllName = "SoftLattice." + pluginName + ".dll";
            return File.Exists(pluginDllName) ? Assembly.LoadFrom(pluginDllName) : null;
        }
    }
}