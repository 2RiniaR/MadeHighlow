using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Methods
{
    public record GeometryInteractAction : InteractAction
    {
        [ItemNotNull]
        [NotNull]
        public ValueObjectList<GeometryInteractTarget> Targets { get; init; } =
            ValueObjectList<GeometryInteractTarget>.Empty;

        public Result Run(in Session session)
        {
            throw new NotImplementedException();
        }
    }
}