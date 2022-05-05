using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record GenerateTileAction : IValidatable
    {
        [NotNull] public Tile Initial { get; init; } = Tile.Empty;

        ISimulatable IValidatable.Validate(in IActionContext context)
        {
            return Validate(context);
        }

        [NotNull]
        public GenerateTileResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}