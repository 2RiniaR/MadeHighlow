using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record GenerateAction() : Action(ActionType.Generate)
    {
        [NotNull] public EntityLocator Actor { get; init; } = new();
        [CanBeNull] public Object Initial { get; init; } = null;

        public Result Run(in Session session)
        {
            throw new NotImplementedException();
        }
    }
}