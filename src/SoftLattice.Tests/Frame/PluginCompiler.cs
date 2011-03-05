using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.CSharp;
using System.Linq;

namespace SoftLattice.Tests.Frame
{
    internal class PluginCompiler : IDisposable
    {
        //Assuming it runs in bin/debug
        private const string BaseDirectoryCompileFiles = @"..\..\";
        private readonly CSharpCodeProvider provider;
        private readonly CompilerParameters parms;
        private readonly List<CompilerResults> result = new List<CompilerResults>();
        private readonly string baseDir;

        //private readonly string assemblyName = Path.GetRandomFileName();

        /// <summary>
        /// Default assumes that you run in bin/debug
        /// </summary>
        public PluginCompiler(string baseDir = BaseDirectoryCompileFiles)
        {
            this.baseDir = baseDir;
            provider = new CSharpCodeProvider(new Dictionary<string, string> { { "CompilerVersion", "v4.0" } });
            parms = new CompilerParameters
                      {
                          GenerateExecutable = false,
                          GenerateInMemory = true,
                      };
        }

        
        public PluginCompiler With(params string[] pluginDirs)
        {
            foreach (var pluginDir in pluginDirs)
              CreateCompileResult(pluginDir);
            return this;
        }

        private void CreateCompileResult(string pluginDir)
        {
            var files = Directory.GetFiles(Path.Combine(baseDir,pluginDir), "*.cs");
            result.Add(provider.CompileAssemblyFromFile(parms, files));
            outputErrorsIfAny();
        }


        public PluginCompiler ReferenceThisAssembly(string assembly)
        {
            parms.ReferencedAssemblies.Add(assembly);
            return this;
        }

        public Assembly[] Assemblies
        {
            get
            {
                outputErrorsIfAny();
                return result.Select(cr=>cr.CompiledAssembly).ToArray();
            }
        }

        private void outputErrorsIfAny()
        {
            if (result.Last().Errors.Count > 0)
            {
                foreach (CompilerError e in result.Last().Errors)
                    Console.WriteLine("Error in line {0} in file {1}: {2}", e.Line, e.FileName, e.ErrorText);
            }
        }

        public PluginCompiler StoreAssemblyAs(string fullPath)
        {
            parms.GenerateInMemory = false;
            parms.OutputAssembly = fullPath;
            return this;
        }

        public void Dispose()
        {
            //if (File.Exists(assemblyName))
            //    File.Delete(assemblyName);
        }
    }
}