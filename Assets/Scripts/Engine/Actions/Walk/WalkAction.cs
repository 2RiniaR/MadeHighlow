using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Walk
{
    public record WalkAction() : Action(ActionType.Walk)
    {
        [NotNull] public ObjectLocator Actor { get; init; } = new();
        [NotNull] public ValueObjectList<Step> Steps { get; init; } = ValueObjectList<Step>.Empty;

        public Event Run(in Session session)
        {
            throw new NotImplementedException();
        }
    }
}