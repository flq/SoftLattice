using System;
using System.Reflection;
using SoftLattice.Common;

namespace SoftLattice.Core.Common
{
    public class PluginDescriptor
    {
        private readonly IResourceServices _resourceService;
        private Assembly _assembly;

        public PluginDescriptor(IResourceServices resourceService)
        {
            _resourceService = resourceService;
        }

        internal void SetPluginAssembly(Assembly assembly)
        {
            _assembly = assembly;
        }

        internal void AddResource(string relativePath)
        {
            _resourceService.AddResource(_assembly.FullName, relativePath);
        }
    }
}