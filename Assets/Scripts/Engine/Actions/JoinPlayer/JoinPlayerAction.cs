using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record JoinPlayerAction : Action<JoinPlayerResult>
    {
        [NotNull]
        public override JoinPlayerResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}