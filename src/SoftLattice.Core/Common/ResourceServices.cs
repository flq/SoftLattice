using System;
using System.Windows;
using SoftLattice.Common;

namespace SoftLattice.Core.Common
{
    public class ResourceServices : IResourceServices
    {
        public void AddResource(string assemblyName, string relativePath)
        {
            Application.Current.AddAssemblyResource(assemblyName, relativePath);
        }
    }
}