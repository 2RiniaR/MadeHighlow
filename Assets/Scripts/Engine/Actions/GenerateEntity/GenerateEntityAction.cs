using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record GenerateEntityAction : IValidatable
    {
        [NotNull] public Entity Initial { get; init; } = Entity.Empty;

        ISimulatable IValidatable.Validate(in IActionContext context)
        {
            return Validate(context);
        }

        [NotNull]
        public GenerateEntityResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}