using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record GenerateTileAction([NotNull] Tile InitialTile) : Action<GenerateTileResult>
    {
        public override GenerateTileResult Validate(IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
