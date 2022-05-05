using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record ElevateTileAction : IValidatable
    {
        ISimulatable IValidatable.Validate(in IActionContext context)
        {
            return Validate(context);
        }

        [NotNull]
        public ElevateTileResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}