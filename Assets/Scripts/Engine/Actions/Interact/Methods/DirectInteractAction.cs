using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DirectInteractAction(
        [NotNull] [ItemNotNull] ValueObjectList<DirectInteractTarget> Targets
    ) : InteractAction
    {
        public override InteractResult Validate(IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}