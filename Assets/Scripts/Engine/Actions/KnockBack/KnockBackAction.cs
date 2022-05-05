using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record KnockBackAction : IValidatable
    {
        ISimulatable IValidatable.Validate(in IActionContext context)
        {
            return Validate(context);
        }

        [NotNull]
        public KnockBackResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}