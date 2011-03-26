using NUnit.Framework;
using SoftLattice.Enhancements.Services;
using SoftLattice.Tests.Frame;

namespace SoftLattice.Tests.Unit
{
    [TestFixture]
    public class ConfigReaderTests
    {
        [Test]
        public void TheConfigSectionIsRead()
        {
            var c = new ConfigurationSettingsAppConfiguration();
            var v = c.GetSection<Values>();
            v.ShouldNotBeNull();
            v.Name.ShouldBeEqualTo("John");
            v.Number.ShouldBeEqualTo(23);
        }
    }
}