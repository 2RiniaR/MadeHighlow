using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid
{
    public record SucceedResult(
        [NotNull] EntityID ActorEntityID,
        [NotNull] Direction2D Direction2D,
        [NotNull] EntityStepCost AvailableStepCost
    ) : EntityStepResult
    {
        public override World Simulate(World world)
        {
            throw new NotImplementedException();
        }
    }
}
