using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.ElevateTile
{
    public sealed record SucceedResult(
        ID SourceID,
        [NotNull] Tile Target,
        [NotNull] Elevate Expected,
        [NotNull] [ItemNotNull] ValueList<Interrupt<ElevateTileEffect>> Interrupts,
        [NotNull] Elevate Calculated
    ) : ElevateTileResult
    {
        public override World Simulate(World world)
        {
            var modifiedTarget = Target with { Elevation = Calculated.Caused(Target.Elevation) };
            world = modifiedTarget.UpdateIn(world);
            return world;
        }
    }
}
