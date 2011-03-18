using System;
using System.Linq;
using System.Reflection;
using SoftLattice.Core.Common;

namespace SoftLattice.Core
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var a = new App();

            if (args != null)
            {
                var info = CreateStartupInfo(args);
                if (info != null)
                    a.Properties["override"] = info;
            }
            
            a.InitializeComponent();
            a.Run();
        }

        private static ProgrammaticStartupInfo CreateStartupInfo(string[] args)
        {
            if (args.Length != 1)
                return null;
            var assemblyNames = args[0].Split(';');
            var startupInfo = new ProgrammaticStartupInfo();
            foreach (var assembly in assemblyNames.Select(MakeAssembly).Where(a=> a != null))
                startupInfo.AddAssembly(assembly);
            return startupInfo;
        }

        private static Assembly MakeAssembly(string name)
        {
            try
            {
                return Assembly.LoadFrom(name);
            }
            catch
            {
                // If the passed in name does not lead to a valid assembly, we do not care at all.
                return null;
            }
        }
    }
}