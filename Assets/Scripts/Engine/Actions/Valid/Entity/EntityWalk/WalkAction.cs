using System;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EntityStep;

namespace RineaR.MadeHighlow.Actions
{
    public record WalkAction(
        [NotNull] EntityID ActorEntityID,
        [NotNull] [ItemNotNull] ValueList<EntityStepAction> StepActions
    ) : ValidAction<WalkResult>
    {
        protected override WalkResult EvaluateBody(IHistory history)
        {
            throw new NotImplementedException();
        }
    }
}
