using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Interact.Methods
{
    public record DirectInteractAction : InteractAction
    {
        [ItemNotNull]
        [NotNull]
        public ValueObjectList<DirectInteractTarget> Targets { get; init; } =
            ValueObjectList<DirectInteractTarget>.Empty;

        public Event Run(in Session session)
        {
            throw new NotImplementedException();
        }
    }
}