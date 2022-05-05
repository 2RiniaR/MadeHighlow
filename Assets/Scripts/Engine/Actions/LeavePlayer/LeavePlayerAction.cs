using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record LeavePlayerAction : IValidatable
    {
        ISimulatable IValidatable.Validate(in IActionContext context)
        {
            return Validate(context);
        }

        [NotNull]
        public LeavePlayerResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}