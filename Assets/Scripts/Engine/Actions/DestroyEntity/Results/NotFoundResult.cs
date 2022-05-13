using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public record NotFoundResult([NotNull] EntityID TargetID) : DestroyEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
