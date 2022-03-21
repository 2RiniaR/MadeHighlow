using System.Collections.Immutable;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Actions;
using RineaR.MadeHighlow.Engine.Subjects.Objects.Components;

namespace RineaR.MadeHighlow.Engine.Environments
{
    public record ComponentsRegistration
    {
        [NotNull]
        private ImmutableDictionary<ComponentType, IUnitStatusEffector> UnitStatusEffectors { get; init; } =
            ImmutableDictionary<ComponentType, IUnitStatusEffector>.Empty;
    }
}