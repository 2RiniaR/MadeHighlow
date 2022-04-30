using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Interact.Methods
{
    public record GeometryInteractAction : InteractAction
    {
        [ItemNotNull]
        [NotNull]
        public ValueObjectList<GeometryInteractTarget> Targets { get; init; } =
            ValueObjectList<GeometryInteractTarget>.Empty;

        public Event Run(in Session session)
        {
            throw new NotImplementedException();
        }
    }
}