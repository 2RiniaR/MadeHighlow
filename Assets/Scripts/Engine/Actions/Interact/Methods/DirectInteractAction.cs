using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Methods
{
    public record DirectInteractAction : InteractAction
    {
        [ItemNotNull]
        [NotNull]
        public ValueObjectList<DirectInteractTarget> Targets { get; init; } =
            ValueObjectList<DirectInteractTarget>.Empty;

        public ISimulatable Run(in Session session)
        {
            throw new NotImplementedException();
        }
    }
}