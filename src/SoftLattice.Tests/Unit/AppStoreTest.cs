using System;
using System.IO;
using NUnit.Framework;
using SoftLattice.Enhancements.Services;
using SoftLattice.Tests.Frame;

namespace SoftLattice.Tests.Unit
{
    [TestFixture]
    public class AppStoreTest
    {
        private XmlApplicationStore _store;

        [TestFixtureSetUp]
        public void Given()
        {
            File.Delete(XmlApplicationStore.FullPath);
            _store = new XmlApplicationStore();
            PutDefault();
        }

        private void PutDefault()
        {
            _store.Put("config", new Values { Name = "Arthur", Number = 42 });
        }

        [Test]
        public void TheConfigValueIsRetrievable()
        {
            var values = _store.Get<Values>("config");
            values.ShouldNotBeNull();
        }

        [Test]
        public void TheConfigValuesWereCorrectlyRetrieved()
        {
            var values = _store.Get<Values>("config");
            values.Name.ShouldBeEqualTo("Arthur");
            values.Number.ShouldBeEqualTo(42);
        }

        [Test]
        public void AValueIsOverwritable()
        {
            _store.Put("config", new Values { Name = "Vogon", Number = 21 });
            var values = _store.Get<Values>("config");
            values.ShouldNotBeNull();
            values.Name.ShouldBeEqualTo("Vogon");
            values.Number.ShouldBeEqualTo(21);

            PutDefault();
        }

        [Test]
        public void PrimitiveValuesCanBePut_String()
        {
            _store.Put("url", "http://localhost");
            var url = _store.Get<string>("url");
            url.ShouldNotBeNull();
            url.ShouldBeEqualTo("http://localhost");
        }

        [Test]
        public void PrimitiveValuesCanBePut_Date()
        {
            _store.Put("init", new DateTime(2011,1,1));
            var dateTime = _store.Get<DateTime>("init");
            dateTime.ShouldBeEqualTo(new DateTime(2011,1,1));
        }
    }


    public class Values
    {
        public string Name { get; set; }
        public decimal Number { get; set; }
    }
}