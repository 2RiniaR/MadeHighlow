using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record LeavePlayerAction : Action<LeavePlayerResult>
    {
        [NotNull]
        public override LeavePlayerResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}