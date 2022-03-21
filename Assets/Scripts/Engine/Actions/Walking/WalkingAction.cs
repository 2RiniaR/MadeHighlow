using System;
using System.Collections.Immutable;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Events;
using RineaR.MadeHighlow.Engine.Subjects.Objects;

namespace RineaR.MadeHighlow.Engine.Actions.Walking
{
    public record WalkingAction() : Action(ActionType.Walking)
    {
        [NotNull] public ObjectLocator Actor { get; init; } = new();
        [NotNull] public ImmutableList<Step> Steps { get; init; } = ImmutableList<Step>.Empty;

        public override EventTimeline Run(in Session session)
        {
            throw new NotImplementedException();
        }
    }
}