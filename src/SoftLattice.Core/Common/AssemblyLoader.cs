using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Linq;

namespace SoftLattice.Core.Common
{
    public class AssemblyLoader
    {
        private static readonly string DefaultBaseDir = AppDomain.CurrentDomain.BaseDirectory;
        private readonly string _baseDir;
        private readonly List<Assembly> _assemblies = new List<Assembly>();

        public AssemblyLoader() : this(DefaultBaseDir)
        {   
        }

        private AssemblyLoader(string baseDir)
        {
            _baseDir = baseDir;
            SetupAssemblies();
        }

        public IEnumerable<Assembly> Assemblies { get { return _assemblies; } }

        private void SetupAssemblies()
        {
            _assemblies.Add(GetType().Assembly); // entry point of the app

            foreach (var dll in ConsideredDlls())
            {
                try
                {
                    var assembly = Assembly.LoadFrom(dll);
                    _assemblies.Add(assembly);
                }
                catch (Exception x)
                {
                    Debug.WriteLine("Failure to load " + dll);
                    Debug.WriteLine(string.Format("{0}:{1}", x.GetType().Name, x.Message));
                }
            }
        }

        private IEnumerable<string> ConsideredDlls()
        {
            foreach (var f in Directory.GetFiles(_baseDir, "*.dll").Where(IsSoftLatticeAssembly))
                yield return f;
            foreach (var f in Directory.GetFiles(_baseDir, "*.exe").Where(IsSoftLatticeAssembly))
                    yield return f;
        }

        private static bool IsSoftLatticeAssembly(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName).IsSoftLatticeAssembly();
        }
    }
}