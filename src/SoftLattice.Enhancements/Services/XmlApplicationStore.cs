using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using SoftLattice.Common;
using System.Linq;

namespace SoftLattice.Enhancements.Services
{
    public class XmlApplicationStore : IApplicationStorage
    {
        // ReSharper disable PossibleNullReferenceException
        // Here we make clear assumptions about the structure of the xml file
        // which we will not check every time.

        private static readonly string _root;

        static XmlApplicationStore()
        {
            _root = AppDomain.CurrentDomain.BaseDirectory;
        }

        public static string FullPath { get { return Path.Combine(_root, "appstore.xml"); } }

        public XmlApplicationStore()
        {
            if (!File.Exists(FullPath))
                CreateFile();
        }

        public T Get<T>(string key)
        {
            var d = GetStore();
            var element = GetSlot(key, d);
            if (element == null)
                return default(T);
            var o = DeserializeElement<T>(element);
            return (T)o;
        }

        public void Put(string key, object value)
        {
            var d = GetStore();
            var element = PrepareSlot(key, d);
            SerializeElement(value, element);
            d.Save(FullPath);
        }

        private static object DeserializeElement<T>(XElement element)
        {
            var s = new XmlSerializer(typeof(T));
            return s.Deserialize(element.DescendantNodes().First().CreateReader());
        }

        private static void SerializeElement(object value, XElement element)
        {
            var s = new XmlSerializer(value.GetType());
            var ms = new MemoryStream();
            s.Serialize(ms, value);
            ms.Seek(0, SeekOrigin.Begin);
            using (var w = element.CreateWriter())
            {
                w.WriteNode(new XmlTextReader(ms), false);
            }
        }

        private static XElement PrepareSlot(string key, XDocument store)
        {
            var element = GetSlot(key, store);
            if (element == null)
            {
                element = new XElement("slot");
                element.Add(new XAttribute("key", key));
                store.Root.Add(element);
            }
            else
            {
                element.RemoveNodes();
            }
            return element;
        }

        private static XElement GetSlot(string key, XDocument store)
        {
            return store.Root.Descendants("slot").FirstOrDefault(e => e.Attribute("key").Value.Equals(key));
        }

        private static void CreateFile()
        {
            var d = new XDocument(new XElement("appstore"));
            d.Save(FullPath);
        }

        private static XDocument GetStore()
        {
            var d = XDocument.Load(FullPath);
            return d;
        }

        // ReSharper restore PossibleNullReferenceException
    }
}