using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using SoftLattice.Common.Resources;

namespace SoftLattice.Core.Common
{
    public class ResourceServices : IResourceServices
    {
        private readonly List<DataTemplate> _dataTemplates = new List<DataTemplate>();
        private readonly Dictionary<Type,DataTemplate> _templateByType = new Dictionary<Type, DataTemplate>();

        public ResourceLoader GetLoaderFor(Assembly assembly)
        {
            return new ResourceLoader(assembly);
        }

        public void RegisterDataTemplates(ResourceDictionary dictionary)
        {
            foreach (var item in dictionary.Cast<DictionaryEntry>().Where(item => item.Value is DataTemplate))
            {
                var dataTemplate = (DataTemplate)item.Value;
                _dataTemplates.Add(dataTemplate);
                if (dataTemplate.DataType != null && dataTemplate.DataType is Type)
                    _templateByType.Add((Type)dataTemplate.DataType, dataTemplate);
            }
        }

        public DataTemplate this[Type dataType]
        {
            get
            {
                DataTemplate t;
                var success = _templateByType.TryGetValue(dataType, out t);
                return success ? t : null;
            }
        }

        public ReadOnlyCollection<DataTemplate> DataTemplates
        {
            get { return _dataTemplates.AsReadOnly(); }
        }
    }
}