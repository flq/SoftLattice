using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;

namespace SnarkyMark.Core.Composition
{
    public interface IDiscovery
    {
        IEnumerable<ComposablePartDefinition> BuildPartDefinitions(IEnumerable<Type> types);
    }
}
