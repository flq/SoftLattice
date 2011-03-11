using NUnit.Framework;
using SoftLattice.Common.Resources;
using SoftLattice.Tests.Frame;

namespace SoftLattice.Tests.Unit
{
    [TestFixture]
    public class ResourceReaderTests
    {
        private ResourceLoader reader;

        [TestFixtureSetUp]
        public void Given()
        {
            reader = new ResourceLoader("PluginA".Load());
        }

        [Test]
        public void CorrectlyFindsAllResources()
        {
            var resourceNames = reader.GetResourceNames();
            resourceNames.ShouldHaveCount(3);
        }
    }
}