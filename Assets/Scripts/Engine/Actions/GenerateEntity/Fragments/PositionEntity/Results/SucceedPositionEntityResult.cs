using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record SucceedPositionEntityResult
        ([NotNull] Entity InitialEntity, [NotNull] Entity PositionedEntity) : PositionEntityResult
    {
        public override World Simulate(World world)
        {
            throw new NotImplementedException();
        }
    }
}
