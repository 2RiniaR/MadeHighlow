using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateTile
{
    public record SucceedResult
        ([NotNull] CreateTileAction Action, [NotNull] CreateTileProcess Process) : CreateTileResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
