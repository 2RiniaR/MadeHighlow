using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record HealAction : IValidatable
    {
        ISimulatable IValidatable.Validate(in IActionContext context)
        {
            return Validate(context);
        }

        [NotNull]
        public HealResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}