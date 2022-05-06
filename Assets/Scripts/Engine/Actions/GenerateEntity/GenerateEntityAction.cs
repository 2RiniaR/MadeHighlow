using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record GenerateEntityAction([NotNull] in Entity InitialEntity) : Action<GenerateEntityResult>
    {
        public override GenerateEntityResult Validate(in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}