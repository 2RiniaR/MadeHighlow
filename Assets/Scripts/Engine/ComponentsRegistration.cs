using System.Collections.Immutable;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow
{
    public record ComponentsRegistration
    {
        [NotNull]
        private ImmutableDictionary<ComponentType, IUnitStatusEffector> UnitStatusEffectors { get; init; } =
            ImmutableDictionary<ComponentType, IUnitStatusEffector>.Empty;
    }
}