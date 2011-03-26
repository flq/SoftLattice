using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Caliburn.Micro;
using SoftLattice.Common;
using System.Linq;

namespace SoftLattice.Enhancements.Models
{
    public class TemplatingConductor : Conductor<object>.Collection.AllActive
    {
        private readonly Dictionary<string, PropertyInfo> _regions;
        private IDisposable _disposable;


        public TemplatingConductor(IObservable<ActivateRegionMsg> activateRegionMessages, string templatingInstance)
        {
            _regions = FindRegions();

            _disposable = activateRegionMessages
                .Where(msg => msg.TemplatingInstance == templatingInstance)
                .ObserveOnDispatcher()
                .Subscribe(OnActivateRegion);

        }

        private void OnActivateRegion(ActivateRegionMsg msg)
        {
            PropertyInfo pi;
            var sucess = _regions.TryGetValue(msg.RegionName, out pi);
            var isPropertyAssignableFromViewModel = pi.PropertyType.IsAssignableFrom(msg.ViewModelType);
            if (sucess)
            {
                if (isPropertyAssignableFromViewModel)
                {
                    pi.SetValue(this, msg.ViewModel, null);
                    NotifyOfPropertyChange(pi.Name);
                }
                else
                {
                    Debug.WriteLine("Viewmodel type " + msg.ViewModelType.Name + " is not assignable to property " + GetType().Name + "." + pi.Name);
                }
            }
            else
            {
                Debug.WriteLine("Region of name " + msg.RegionName + " could not be found on " + GetType().Name);
            }
        }

        private Dictionary<string,PropertyInfo> FindRegions()
        {
            var pis = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            WarnForPropertiesWithNoSetter(pis.Where(pi => pi.GetSetMethod(true) == null));
            return RegisterProperties(pis.Where(pi => pi.GetSetMethod(true) != null));
        }

        private void WarnForPropertiesWithNoSetter(IEnumerable<PropertyInfo> propertyInfos)
        {
            foreach (var pi in propertyInfos)
                Debug.WriteLine("Property " + pi.Name + " on type " + GetType().Name + " will not be considered as targettable region, as no setter could be defined");
        }

        private static Dictionary<string,PropertyInfo> RegisterProperties(IEnumerable<PropertyInfo> propertyInfos)
        {
            return propertyInfos.ToDictionary(pi => pi.Name, pi => pi);
        }
    }
}