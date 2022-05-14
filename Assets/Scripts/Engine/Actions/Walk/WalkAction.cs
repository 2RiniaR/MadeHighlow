using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record WalkAction(
        [NotNull] EntityID ActorEntityID,
        [NotNull] [ItemNotNull] ValueList<StepAction> StepActions
    ) : Action<WalkResult>
    {
        protected override WalkResult EvaluateBody(IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
