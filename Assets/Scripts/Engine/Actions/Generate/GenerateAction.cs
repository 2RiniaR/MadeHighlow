using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Generate
{
    public record GenerateAction() : Action(ActionType.Generate)
    {
        [NotNull] public ObjectLocator Actor { get; init; } = new();
        [CanBeNull] public Object Initial { get; init; } = null;

        public Event Run(in Session session)
        {
            throw new NotImplementedException();
        }
    }
}