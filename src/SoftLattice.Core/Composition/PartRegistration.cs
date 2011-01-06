using System;
using System.ComponentModel.Composition.Primitives;
using SnarkyMark.Core.Support;

namespace SnarkyMark.Core.Composition
{
    internal abstract class PartRegistration
    {
        public static SinglePartRegistration<TIMPL> Type<TIMPL>()
        {
            return new SinglePartRegistration<TIMPL>();
        }

        public static MultiplePartRegistration TypesDerivedFrom<TBASE>() where TBASE : class
        {
            return TypesDerivedFrom(typeof(TBASE));
        }

        public static MultiplePartRegistration TypesDerivedFrom(Type baseType)
        {
            return new MultiplePartRegistration(type => !type.IsAbstract && !type.IsInterface && baseType.IsAssignableFrom(type));
        }

        public static MultiplePartRegistration TypesThatImplement<TINTERFACE>()
        {
            return TypesThatImplement(typeof(TINTERFACE));
        }

        public static MultiplePartRegistration TypesThatImplement(Type interfaceType)
        {
            return new MultiplePartRegistration(type => type.Implements(interfaceType));
        }

        public static MultiplePartRegistration Types(Predicate<Type> selector)
        {
            return new MultiplePartRegistration(selector);
        }

        internal abstract bool IsMultipleRegistration { get; }

        internal abstract ComposablePartDefinition CreatePartFor(Type type, Convention convention);
    }
}
