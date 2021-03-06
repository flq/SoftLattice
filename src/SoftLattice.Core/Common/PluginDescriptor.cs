using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using SoftLattice.Common.Resources;
using System.Linq;

namespace SoftLattice.Core.Common
{
    public class PluginDescriptor
    {
        private readonly IResourceServices _resourceService;
        private readonly Application _app;
        private Assembly _assembly;
        private ResourceLoader _resLoader;
        private readonly List<Type> _subscriberTypes = new List<Type>();

        public PluginDescriptor(IResourceServices resourceService, Application app)
        {
            _resourceService = resourceService;
            _app = app;
        }

        internal void SetPluginAssembly(Assembly assembly)
        {
            _assembly = assembly;
            _resLoader = _resourceService.GetLoaderFor(_assembly);
        }

        internal void AddResource(string relativePath)
        {
            var dict = _resLoader.GetDictionary(relativePath);
            _resourceService.RegisterDataTemplates(dict);
            _app.Resources.MergedDictionaries.Add(dict);
        }

        public void AddResource(Func<string, bool> pathPredicate)
        {
            foreach (var path in _resLoader.GetResourceNames().Where(pathPredicate))
            {
                AddResource(path + ".xaml");
            }
        }

        public void AddMessageListenerType(Type subscriberType)
        {
            _subscriberTypes.Add(subscriberType);
        }

        public IEnumerable<Type> MessageListenerTypes { get { return _subscriberTypes; } }
    }
}