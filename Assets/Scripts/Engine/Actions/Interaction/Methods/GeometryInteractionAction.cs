using System;
using System.Collections.Immutable;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Events;

namespace RineaR.MadeHighlow.Engine.Actions.Interaction.Methods
{
    public record GeometryInteractionAction : InteractionAction
    {
        [ItemNotNull]
        [NotNull]
        public ImmutableList<GeometryInteractionTarget> Targets { get; init; } =
            ImmutableList<GeometryInteractionTarget>.Empty;

        public override EventTimeline Run(in Session session)
        {
            throw new NotImplementedException();
        }
    }
}