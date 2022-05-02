using System.Collections.Immutable;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow
{
    public record ComponentsRegistration
    {
        [NotNull]
        private ImmutableDictionary<ComponentType, IUnitReferenceEffector> UnitStatusEffectors { get; init; } =
            ImmutableDictionary<ComponentType, IUnitReferenceEffector>.Empty;
    }
}