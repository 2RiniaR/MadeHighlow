using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DamageAction : Action<DamageResult>
    {
        [NotNull]
        public override DamageResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}