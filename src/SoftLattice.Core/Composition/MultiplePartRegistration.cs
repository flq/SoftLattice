using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.ReflectionModel;
using System.Linq;

namespace SnarkyMark.Core.Composition
{
    internal class MultiplePartRegistration : BasePartRegistration<MultiplePartRegistration>
    {
        private readonly Predicate<Type> _selector;

        public MultiplePartRegistration(Predicate<Type> selector)
        {
            _selector = selector;
        }

        #region CreationPolicy

        private readonly Dictionary<Type, CreationPolicy> _customCreationPolicy = new Dictionary<Type, CreationPolicy>();

        public MultiplePartRegistration CreationPolicyFor<TARGET>(CreationPolicy creationPolicy)
        {
            _customCreationPolicy[typeof(TARGET)] = creationPolicy;
            return this;
        }

        #endregion

        #region Excluding

        private readonly HashSet<Type> _excludingList = new HashSet<Type>();
        private readonly List<Predicate<Type>> _excludingPredicateList = new List<Predicate<Type>>();

        public MultiplePartRegistration Excluding<TARGET>()
        {
            _excludingList.Add(typeof(TARGET));
            return this;
        }

        public MultiplePartRegistration Excluding(Type typeToExclude)
        {
            _excludingList.Add(typeToExclude);
            return this;
        }

        public MultiplePartRegistration Excluding(Predicate<Type> typePredicate)
        {
            _excludingPredicateList.Add(typePredicate);
            return this;
        }

        #endregion

        internal override bool IsMultipleRegistration { get { return true; } }

        internal override ComposablePartDefinition CreatePartFor(Type type, Convention convention)
        {
            if (!_selector(type))
                return null;

            if (IsExcluded(type))
                return null;

            bool isDisposalRequired = typeof(IDisposable).IsAssignableFrom(type);
            var exports = new List<ExportDefinition>();
            var imports = new List<ImportDefinition>();
            var metadata = new Dictionary<string, object>(PartMetadataDict);

            // CreationPolicy
            var policy = _customCreationPolicy.Keys.Where(t => t.IsAssignableFrom(type)).Select(t => _customCreationPolicy[t])
                .FirstOrDefault();
            if (policy != CreationPolicy.Any)
                metadata[CompositionConstants.PartCreationPolicyMetadataName] = policy;

            CreateExportDefinitions(type, convention, exports);
            CreateImportDefinitions(type, convention, imports);

            return ReflectionModelServices.CreatePartDefinition(
                new Lazy<Type>(() => type),
                isDisposalRequired,
                new Lazy<IEnumerable<ImportDefinition>>(() => imports),
                new Lazy<IEnumerable<ExportDefinition>>(() => exports),
                new Lazy<IDictionary<string, object>>(() => metadata),
                null);
        }


        internal override MultiplePartRegistration GetMostDerived()
        {
            return this;
        }

        private bool IsExcluded(Type type)
        {
            if (_excludingList.Contains(type)) return true;

            return _excludingPredicateList.Any(ex => ex(type));
        }
    }

}
