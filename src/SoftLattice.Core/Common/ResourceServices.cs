using System.Reflection;
using SoftLattice.Common.Resources;

namespace SoftLattice.Core.Common
{
    public class ResourceServices : IResourceServices
    {
        public ResourceLoader GetLoaderFor(Assembly assembly)
        {
            return new ResourceLoader(assembly);
        }
    }
}