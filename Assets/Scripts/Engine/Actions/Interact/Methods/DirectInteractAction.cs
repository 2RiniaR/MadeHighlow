using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DirectInteractAction(
        [NotNull] [ItemNotNull] in ValueObjectList<DirectInteractTarget> Targets
    ) : InteractAction
    {
        public override InteractResult Validate(in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}