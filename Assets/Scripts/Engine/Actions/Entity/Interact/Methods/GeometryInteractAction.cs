using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record GeometryInteractAction
        ([NotNull] [ItemNotNull] ValueList<GeometryInteractTarget> Targets) : InteractAction
    {
        protected override InteractResult EvaluateBody(IHistory context)
        {
            throw new NotImplementedException();
        }
    }
}
