using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace SoftLattice.Common.Resources
{
    /// <summary>
    /// Resource access on an assembly
    /// </summary>
    public class ResourceLoader
    {
        private readonly Assembly _assembly;

        public ResourceLoader(Assembly assembly)
        {
            _assembly = assembly;
        }

        /// <summary>
        /// Enumerate the names of all resources contained in the provided assembly
        /// </summary>
        public IEnumerable<string> GetResourceNames()
        {
            var asm = _assembly;
            var resName = asm.GetName().Name + ".g.resources";
            using (var stream = asm.GetManifestResourceStream(resName))
            using (var reader = new System.Resources.ResourceReader(stream))
            {
                return reader.Cast<DictionaryEntry>().Select(entry => ((string)entry.Key).Replace(".baml","")).ToArray();
            }
        }

        /// <summary>
        /// Kindly taken from http://marcmelvin.com/?p=70. 
        /// Returns a ResourceDictionary based on a relative path
        /// </summary>
        public ResourceDictionary GetDictionary(string path)
        {
            if (!UriParser.IsKnownScheme("pack"))
                UriParser.Register(new GenericUriParser(GenericUriParserOptions.GenericAuthority), "pack", -1);

            var dict = new ResourceDictionary();
            var uri = new Uri("/" + _assembly.GetName().Name + ";component/" + path, UriKind.Relative);
            dict.Source = uri;
            return dict;
        }
    }
}