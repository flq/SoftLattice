using NUnit.Framework;
using StructureMap;
using SoftLattice.Tests.Frame;

namespace SoftLattice.Tests.Thirdparty
{
    [TestFixture]
    public class StructuremapTests
    {
        private IContainer cnt;

        [TestFixtureSetUp]
        public void Given()
        {
            cnt = new Container(ce =>
                                    {
                                        ce.For<IFoo>().Use<Foo>();
                                        ce.Forward<IFa, IFoo>();
                                        ce.Forward<IFo, IFoo>();
                                    });
        }

        [Test]
        public void ForwardIsntWorking()
        {
            Assert.Throws<StructureMapException>(()=> cnt.GetInstance<IFa>());
            
        }
    }

    public class Foo : IFoo { }
    public interface IFoo : IFa, IFo { }
    public interface IFo { }
    public interface IFa { }

    
}