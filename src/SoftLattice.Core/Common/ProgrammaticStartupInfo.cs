using System.Collections.Generic;
using System.Reflection;

namespace SoftLattice.Core.Common
{
    /// <summary>
    /// USed by integration tests to pass in startup parameters to SoftwareLAttice core
    /// </summary>
    internal class ProgrammaticStartupInfo
    {
        private readonly List<Assembly> _pluginAssemblies = new List<Assembly>();

        public IEnumerable<Assembly> PluginAssemblies { get { return _pluginAssemblies;  } }

        public void AddAssembly(Assembly assembly)
        {
            _pluginAssemblies.Add(assembly);
        }
    }
}