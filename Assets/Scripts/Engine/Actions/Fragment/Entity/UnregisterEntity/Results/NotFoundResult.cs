using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.UnregisterEntity
{
    public record NotFoundResult([NotNull] UnregisterEntityAction Action) : UnregisterEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
