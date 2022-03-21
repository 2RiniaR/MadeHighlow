using System;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Events;
using RineaR.MadeHighlow.Engine.Subjects.Objects;
using Object = RineaR.MadeHighlow.Engine.Subjects.Objects.Object;

namespace RineaR.MadeHighlow.Engine.Actions.Generation
{
    public record GenerationAction() : Action(ActionType.Generation)
    {
        [NotNull] public ObjectLocator Actor { get; init; } = new();
        [CanBeNull] public Object Initial { get; init; } = null;

        public override EventTimeline Run(in Session session)
        {
            throw new NotImplementedException();
        }
    }
}