using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record SucceedStepResult(
        [NotNull] in EntityID ActorEntityID,
        [NotNull] in Direction2D Direction2D,
        [NotNull] [ItemNotNull] in ValueObjectList<StepOutReaction> StepOutReactions,
        [NotNull] [ItemNotNull] in ValueObjectList<StepInReaction> StepInReactions,
        [NotNull] [ItemNotNull] in ValueObjectList<Result> AfterActionResults,
        [NotNull] in StepCost AvailableStepCost
    ) : StepResult
    {
        public override World Simulate(in World world)
        {
            throw new NotImplementedException();
        }
    }
}