using System;
using System.Reflection;
using System.Windows;

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

        /// <summary>
        /// Kindly taken from http://marcmelvin.com/?p=70. Adds a ResourceDictionary to the current application
        /// </summary>
        public static void AddAssemblyResource(this Application app, string assemblyName, string path)
        {
            if (!UriParser.IsKnownScheme("pack"))
                UriParser.Register(new GenericUriParser(GenericUriParserOptions.GenericAuthority), "pack", -1);

            var dict = new ResourceDictionary();
            var uri = new Uri("/" + assemblyName + ";component/" + path, UriKind.Relative);
            dict.Source = uri;
            app.Resources.MergedDictionaries.Add(dict);
        }
    }
}