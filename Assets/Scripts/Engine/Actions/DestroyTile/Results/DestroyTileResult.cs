using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DestroyTileResult : ISimulatable
    {
        [NotNull] public TileEnsuredID Actor { get; init; } = new();

        public World Simulate(in World world)
        {
            return Actor.Delete(world);
        }
    }
}