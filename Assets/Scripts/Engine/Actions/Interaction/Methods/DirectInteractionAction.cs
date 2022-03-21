using System;
using System.Collections.Immutable;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Events;

namespace RineaR.MadeHighlow.Engine.Actions.Interaction.Methods
{
    public record DirectInteractionAction : InteractionAction
    {
        [ItemNotNull]
        [NotNull]
        public ImmutableList<DirectInteractionTarget> Targets { get; init; } =
            ImmutableList<DirectInteractionTarget>.Empty;

        public override EventTimeline Run(in Session session)
        {
            throw new NotImplementedException();
        }
    }
}