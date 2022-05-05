using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record HealAction : Action<HealResult>
    {
        [NotNull]
        public override HealResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}