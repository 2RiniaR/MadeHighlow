using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DeathAction : IValidatable
    {
        ISimulatable IValidatable.Validate(in IActionContext context)
        {
            return Validate(context);
        }

        [NotNull]
        public DeathResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}