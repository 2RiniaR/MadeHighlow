using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DestroyEntityResult : Result
    {
        [NotNull] public EntityID Actor { get; init; } = new();

        public override World Simulate(in World world)
        {
            return Actor.DeleteFrom(world);
        }
    }
}