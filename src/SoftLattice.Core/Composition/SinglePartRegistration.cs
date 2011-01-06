using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.ReflectionModel;
using System.Linq.Expressions;

namespace SnarkyMark.Core.Composition
{
    internal class SinglePartRegistration<TIMPL> : BasePartRegistration<SinglePartRegistration<TIMPL>>
    {
        public SinglePartRegistration<TIMPL> Exporting<TSVC>()
        {
            var expConfig = CreateMemberExportConfig(typeof(TIMPL), typeof(TSVC));

            ExportConfigs.Add(expConfig);

            return this;
        }

        public SinglePartRegistration<TIMPL> Exporting<TSVC>(Action<MemberExportConfig, Type> config)
        {
            var expConfig = CreateMemberExportConfig(typeof(TIMPL), typeof(TSVC));

            config(expConfig, typeof(TIMPL));

            ExportConfigs.Add(expConfig);

            return this;
        }

        public SinglePartRegistration<TIMPL> ImportingMember(
            Expression<Func<TIMPL, object>> propertyOrField)
        {
            var importConfig = CreateMemberImportConfig(propertyOrField);

            ImportConfigs.Add(importConfig);

            return this;
        }

        public SinglePartRegistration<TIMPL> ImportingMember(
            Expression<Func<TIMPL, object>> propertyOrField,
            Action<MemberImportConfig> config)
        {
            var importConfig = CreateMemberImportConfig(propertyOrField);

            config(importConfig);
            
            ImportConfigs.Add(importConfig);

            return this;
        }

        internal override bool IsMultipleRegistration { get { return false; } }

        internal override ComposablePartDefinition CreatePartFor(Type type, Convention convention)
        {
            if (type != null)
                throw new ArgumentException("Type must be null", "type");

            var partType = typeof(TIMPL);
            bool isDisposalRequired = typeof(IDisposable).IsAssignableFrom(partType);
            var exports = new List<ExportDefinition>();
            var imports = new List<ImportDefinition>();
            var metadata = new Dictionary<string, object>();

            CreateExportDefinitions(type, convention, exports);
            CreateImportDefinitions(type, convention, imports);

            return ReflectionModelServices.CreatePartDefinition(
                new Lazy<Type>(() => partType),
                isDisposalRequired,
                new Lazy<IEnumerable<ImportDefinition>>(() => imports),
                new Lazy<IEnumerable<ExportDefinition>>(() => exports),
                new Lazy<IDictionary<string, object>>(() => metadata),
                null);
        }

        internal override SinglePartRegistration<TIMPL> GetMostDerived()
        {
            return this;
        }
    }
}
