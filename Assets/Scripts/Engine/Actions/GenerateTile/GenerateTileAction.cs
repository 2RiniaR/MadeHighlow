using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record GenerateTileAction : Action<GenerateTileResult>
    {
        [NotNull] public Tile Initial { get; init; } = Tile.Empty;


        [NotNull]
        public override GenerateTileResult Validate([NotNull] in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}