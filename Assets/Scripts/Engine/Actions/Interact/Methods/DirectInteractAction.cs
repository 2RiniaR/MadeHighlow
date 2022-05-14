using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record DirectInteractAction([NotNull] [ItemNotNull] ValueList<DirectInteractTarget> Targets) : InteractAction
    {
        protected override InteractResult EvaluateBody(IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
