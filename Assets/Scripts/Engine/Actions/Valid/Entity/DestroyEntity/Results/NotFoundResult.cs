using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.DestroyEntity
{
    public record NotFoundResult([NotNull] EntityID TargetID) : DestroyEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
