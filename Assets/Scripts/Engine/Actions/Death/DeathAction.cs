using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DeathAction : Action<DeathResult>
    {
        [NotNull]
        public override DeathResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}