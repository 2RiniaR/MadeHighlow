using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid
{
    public record GeometryInteractAction
        ([NotNull] [ItemNotNull] ValueList<GeometryInteractTarget> Targets) : InteractAction
    {
        protected override InteractResult EvaluateBody(IHistory history)
        {
            throw new NotImplementedException();
        }
    }
}
