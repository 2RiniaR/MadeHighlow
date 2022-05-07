using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DestroyTileAction([NotNull] TileID TargetTileID) : Action<DestroyTileResult>
    {
        public override DestroyTileResult Validate(IActionContext context)
        {
            return new DestroyTileResult(TargetTileID);
        }
    }
}