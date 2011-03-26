using System;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SoftLattice.Enhancements
{
    public class ConfigHandler<T> : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            var sr = new StringReader(section.OuterXml);
            var s = new XmlSerializer(typeof(T));
            return (T)s.Deserialize(sr);
        }
    }
}