using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteTile
{
    public record NotFoundResult([NotNull] DeleteTileAction Action) : DeleteTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
