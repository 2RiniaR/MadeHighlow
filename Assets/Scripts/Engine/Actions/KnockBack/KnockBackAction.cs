using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record KnockBackAction : Action<KnockBackResult>
    {
        [NotNull]
        public override KnockBackResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}