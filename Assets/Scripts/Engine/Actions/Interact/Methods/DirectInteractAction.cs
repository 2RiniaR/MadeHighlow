using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DirectInteractAction : InteractAction
    {
        [ItemNotNull]
        [NotNull]
        public ValueObjectList<DirectInteractTarget> Targets { get; init; } =
            ValueObjectList<DirectInteractTarget>.Empty;

        public Result Run(in Session session)
        {
            throw new NotImplementedException();
        }
    }
}