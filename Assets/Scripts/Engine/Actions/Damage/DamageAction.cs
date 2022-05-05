using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DamageAction : IValidatable
    {
        ISimulatable IValidatable.Validate(in IActionContext context)
        {
            return Validate(context);
        }

        [NotNull]
        public DamageResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}