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

        [SetUp]
        public void Given()
        {
            cnt = new Container(ce =>
                                    {
                                        ce.For<IFoo>().Use<Foo>();
                                        ce.Forward<IFoo, IFa>();
                                        ce.Forward<IFoo, IFo>();
                                    });
        }

        [Test]
        public void ForwardWorks()
        {
            var i = cnt.GetInstance<IFa>();
            i.ShouldBeOfType<Foo>();
        }

        [Test]
        public void WhatIsWithWith()
        {
            var dick = (Bar)cnt.With(new Baz {Message = "A"}).GetInstance(typeof (Bar));
            dick.Message.ShouldBeEqualTo("A");
        }

        [Test]
        public void ConfigureOverridesDefault()
        {
            var f = cnt.GetInstance<IFoo>();
            f.ShouldBeOfType<Foo>();
            cnt.Configure(e=>e.For<IFoo>().Use<NewFoo>());
            f = cnt.GetInstance<IFoo>();
            f.ShouldBeOfType<NewFoo>();
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

    public class NewFoo : IFoo
    {
        
    }

    public class Poo
    {
        public Poo(Type bla)
        {
            
        }
    }
}