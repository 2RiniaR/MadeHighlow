using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DestroyTileResult : Result
    {
        [NotNull] public TileEnsuredID Actor { get; init; } = new();

        public override World Simulate(in World world)
        {
            return Actor.DeleteFrom(world);
        }
    }
}