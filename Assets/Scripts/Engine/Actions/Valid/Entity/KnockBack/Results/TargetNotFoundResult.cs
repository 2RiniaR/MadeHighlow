using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public record TargetNotFoundResult([NotNull] KnockBackAction Action) : KnockBackResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
