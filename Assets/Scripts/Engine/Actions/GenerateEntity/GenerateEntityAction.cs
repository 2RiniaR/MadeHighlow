using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record GenerateEntityAction : Action<GenerateEntityResult>
    {
        [NotNull] public Entity Initial { get; init; } = Entity.Empty;


        [NotNull]
        public override GenerateEntityResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}