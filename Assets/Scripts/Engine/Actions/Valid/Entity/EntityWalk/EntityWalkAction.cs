using System;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EntityStep;

namespace RineaR.MadeHighlow.Actions.EntityWalk
{
    public record EntityWalkAction(
        [NotNull] EntityID ActorID,
        [NotNull] EntityWalkRoute Route,
        [NotNull] EntityStepCost Available
    ) : ValidAction<EntityWalkResult>
    {
        protected override EntityWalkResult EvaluateBody(IHistory history)
        {
            throw new NotImplementedException();
        }
    }
}
