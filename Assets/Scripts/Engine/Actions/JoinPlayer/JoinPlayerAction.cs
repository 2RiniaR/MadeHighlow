using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record JoinPlayerAction : IValidatable
    {
        ISimulatable IValidatable.Validate(in IActionContext context)
        {
            return Validate(context);
        }

        [NotNull]
        public JoinPlayerResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}