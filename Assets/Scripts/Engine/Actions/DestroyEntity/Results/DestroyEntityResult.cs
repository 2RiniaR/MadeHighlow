using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DestroyEntityResult([NotNull] EntityID DestroyedEntityID) : Result
    {
        public override World Simulate(World world)
        {
            return DestroyedEntityID.DeleteFrom(world);
        }
    }
}