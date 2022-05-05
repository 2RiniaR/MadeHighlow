using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record TeleportAction : IValidatable
    {
        ISimulatable IValidatable.Validate(in IActionContext context)
        {
            return Validate(context);
        }

        [NotNull]
        public TeleportResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}