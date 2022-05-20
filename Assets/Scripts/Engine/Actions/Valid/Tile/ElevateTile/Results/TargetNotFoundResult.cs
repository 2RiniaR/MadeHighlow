using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.ElevateTile
{
    public record TargetNotFoundResult([NotNull] ElevateTileAction Action) : ElevateTileResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
