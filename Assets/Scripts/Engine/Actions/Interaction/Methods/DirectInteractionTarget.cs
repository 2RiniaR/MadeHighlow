using System.Collections.Immutable;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects.Objects;

namespace RineaR.MadeHighlow.Engine.Actions.Interaction.Methods
{
    public record DirectInteractionTarget
    {
        [NotNull] public ObjectLocator Pointer { get; init; } = new();

        [NotNull]
        [ItemNotNull]
        public ImmutableList<InteractionEffect> Effects { get; init; } = ImmutableList<InteractionEffect>.Empty;
    }
}