using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Linq;

namespace SnarkyMark.Core.Composition
{
    internal abstract class ConventionDefinition : IDiscovery
    {

        private readonly Convention _convention;
        private readonly List<PartRegistration> _registrations = new List<PartRegistration>();

        protected ConventionDefinition(params PartRegistration[] registrations)
            : this(new Convention(), registrations)
        {
        }

        protected ConventionDefinition(Convention customConvention, params PartRegistration[] registrations)
        {
            _convention = customConvention;
            _registrations.AddRange(registrations);
        }

        public void Add(PartRegistration registration)
        {
            _registrations.Add(registration);
        }

        IEnumerable<ComposablePartDefinition> IDiscovery.BuildPartDefinitions(IEnumerable<Type> types)
        {
            var parts = new List<ComposablePartDefinition>();
            var multipleRegs = _registrations.Where(reg => reg.IsMultipleRegistration);
            var singleRegs = _registrations.Where(reg => !reg.IsMultipleRegistration);

            foreach (var reg in singleRegs)
            {
                var part = reg.CreatePartFor(null, _convention);

                if (part == null) throw new Exception(); // "cant" happen

                parts.Add(part);
            }

            parts.AddRange(
              types.SelectMany(type => multipleRegs, (type, reg) => reg.CreatePartFor(type, _convention))
              .Where(part => part != null));

            return parts;
        }
    }
}