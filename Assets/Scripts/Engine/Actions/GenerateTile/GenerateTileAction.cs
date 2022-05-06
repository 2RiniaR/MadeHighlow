using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record GenerateTileAction([NotNull] in Tile InitialTile) : Action<GenerateTileResult>
    {
        public override GenerateTileResult Validate(in IActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}