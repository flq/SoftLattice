using System;
using System.Configuration;
using SoftLattice.Common;

namespace SoftLattice.Enhancements.Services
{
    public class ConfigurationSettingsAppConfiguration : IAppConfiguration
    {
        public T GetSection<T>()
        {
            var o = ConfigurationManager.GetSection(typeof (T).Name);
            return (T) o;
        }
    }
}