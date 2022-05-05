using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record ElevateTileAction : Action<ElevateTileResult>
    {
        [NotNull]
        public override ElevateTileResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}