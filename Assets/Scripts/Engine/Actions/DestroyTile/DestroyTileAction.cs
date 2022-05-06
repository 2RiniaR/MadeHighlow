using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DestroyTileAction([NotNull] in TileID TargetTileID) : Action<DestroyTileResult>
    {
        public override DestroyTileResult Validate(in IActionContext context)
        {
            return new DestroyTileResult(TargetTileID);
        }
    }
}