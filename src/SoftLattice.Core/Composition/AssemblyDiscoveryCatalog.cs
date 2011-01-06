using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using SnarkyMark.Core.Support;

namespace SnarkyMark.Core.Composition
{
    internal class AssemblyDiscoveryCatalog : ComposablePartCatalog
    {
        private readonly object _locker = new object();
        private IQueryable<ComposablePartDefinition> _parts;

        public AssemblyDiscoveryCatalog(params Assembly[] assemblies)
        {
            Assemblies = assemblies;
        }

        private IEnumerable<Assembly> Assemblies { get; set; }

        public override IQueryable<ComposablePartDefinition> Parts
        {
            get
            {
                if (_parts == null)
                {
                    lock (_locker)
                    {
                        if (_parts == null)
                        {
                            var parts = InspectAssembliesAndBuildPartDefinitions();
                            System.Threading.Thread.MemoryBarrier();
                            _parts = parts.AsQueryable();
                        }
                    }
                }

                return _parts;
            }
        }

        private IEnumerable<ComposablePartDefinition> InspectAssembliesAndBuildPartDefinitions()
        {
            var parts = new List<ComposablePartDefinition>();
            var conventionDefinitionType = GetType().Assembly.GetTypes().First(t => t.Implements<IDiscovery>());
            var discovery = (IDiscovery)Activator.CreateInstance(conventionDefinitionType);

            foreach (var discoveredParts in
                Assemblies.Select(assembly => discovery.BuildPartDefinitions(assembly.GetTypes())))
            {
                parts.AddRange(discoveredParts);
            }

            return parts;
        }
    }
}