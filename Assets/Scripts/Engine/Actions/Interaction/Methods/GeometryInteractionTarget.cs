using System.Collections.Immutable;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects.Geometry;

namespace RineaR.MadeHighlow.Engine.Actions.Interaction.Methods
{
    public record GeometryInteractionTarget
    {
        [NotNull] public Position3D Position { get; init; } = new();

        [NotNull]
        [ItemNotNull]
        public ImmutableList<InteractionEffect> Effects { get; init; } = ImmutableList<InteractionEffect>.Empty;
    }
}