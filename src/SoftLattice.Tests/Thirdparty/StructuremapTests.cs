using System;
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

        [Test]
        public void WhatIsWithWith()
        {
            var dick = (Bar)cnt.With(new Baz {Message = "A"}).GetInstance(typeof (Bar));
            dick.Message.ShouldBeEqualTo("A");
        }
    }

    public class Foo : IFoo { }
    public interface IFoo : IFa, IFo { }
    public interface IFo { }
    public interface IFa { }

    public class Bar
    {
        public Bar(Baz baz)
        {
            Message = baz.Message;
        }

        public string Message { get; set; }
    }

    public class Baz : Poo, IFoo
    {
        public Baz() : base(typeof(object))
        {
        }

        public string Message { get; set; }
    }

    public class Poo
    {
        public Poo(Type bla)
        {
            
        }
    }
}