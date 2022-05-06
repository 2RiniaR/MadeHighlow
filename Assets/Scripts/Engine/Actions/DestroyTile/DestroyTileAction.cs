using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DestroyTileAction : Action<DestroyTileResult>
    {
        [NotNull] public TileID EntityID { get; init; } = new();


        [NotNull]
        public override DestroyTileResult Validate([NotNull] in IActionContext context)
        {
            return new DestroyTileResult { Actor = EntityID };
        }
    }
}