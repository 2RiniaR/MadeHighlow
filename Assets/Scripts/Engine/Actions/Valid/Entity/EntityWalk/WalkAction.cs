using System;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Valid.EntityStep;

namespace RineaR.MadeHighlow.Actions.Valid
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
