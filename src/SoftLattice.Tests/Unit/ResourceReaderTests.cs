using System.Windows;
using NUnit.Framework;
using SoftLattice.Common.Resources;
using SoftLattice.Core.Common;
using SoftLattice.Tests.Frame;

namespace SoftLattice.Tests.Unit
{
    [TestFixture]
    public class ResourceReaderTests
    {
        private ResourceLoader reader;
        private IResourceServices service;

        [TestFixtureSetUp]
        public void Given()
        {
            if (Application.Current == null) new Application(); //Awesome

            reader = new ResourceLoader("PluginA".Load());
            service = new ResourceServices();
            var dict = reader.GetDictionary("Plugin.xaml");
            service.RegisterDataTemplates(dict);
        }

        [Test]
        public void CorrectlyFindsAllResources()
        {
            var resourceNames = reader.GetResourceNames();
            resourceNames.ShouldHaveCount(3);
        }

        [Test]
        public void FindsAnyDataTemplates()
        {
            service.DataTemplates.ShouldHaveCount(1);
        }

        [Test]
        public void TryGetTemplateReturnsNullIfNoneFound()
        {
            var template = service[typeof(ResourceReaderTests)];
            template.ShouldBeNull();
        }

        [Test]
        public void GetTemplateReturnsTheRightTemplate()
        {
            var template = service[typeof (string)];
            template.ShouldNotBeNull();
            template.DataType.Equals(typeof (string));
        }
    }
}