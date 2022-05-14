using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid
{
    public record DirectInteractAction([NotNull] [ItemNotNull] ValueList<DirectInteractTarget> Targets) : InteractAction
    {
        protected override InteractResult EvaluateBody(IHistory history)
        {
            throw new NotImplementedException();
        }
    }
}
