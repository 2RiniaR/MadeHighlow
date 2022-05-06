using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DestroyEntityResult([NotNull] in EntityID DestroyedEntityID) : Result
    {
        public override World Simulate(in World world)
        {
            return DestroyedEntityID.DeleteFrom(world);
        }
    }
}