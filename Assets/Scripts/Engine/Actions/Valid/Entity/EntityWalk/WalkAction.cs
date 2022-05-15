using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid
{
    public record WalkAction(
        [NotNull] EntityID ActorEntityID,
        [NotNull] [ItemNotNull] ValueList<StepAction> StepActions
    ) : ValidAction<WalkResult>
    {
        protected override WalkResult EvaluateBody(IHistory history)
        {
            throw new NotImplementedException();
        }
    }
}
