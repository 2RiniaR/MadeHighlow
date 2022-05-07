using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record GenerateEntityAction([NotNull] Entity InitialEntity) : Action<GenerateEntityResult>
    {
        public override GenerateEntityResult Validate(IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
