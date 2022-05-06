using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record GeometryInteractAction(
        [NotNull] [ItemNotNull] in ValueObjectList<GeometryInteractTarget> Targets
    ) : InteractAction
    {
        public override InteractResult Validate(in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}