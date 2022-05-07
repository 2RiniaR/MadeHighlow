using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record GeometryInteractAction(
        [NotNull] [ItemNotNull] ValueObjectList<GeometryInteractTarget> Targets
    ) : InteractAction
    {
        public override InteractResult Validate(IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}