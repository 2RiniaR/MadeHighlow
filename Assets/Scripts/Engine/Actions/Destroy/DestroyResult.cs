using JetBrains.Annotations;
using RineaR.MadeHighlow.Queries;

namespace RineaR.MadeHighlow.Actions
{
    public record DestroyResult() : Result(ActionType.Destroy)
    {
        [NotNull] public EntityLocator Actor { get; init; } = new();

        public override World Simulate(in World world)
        {
            return new DeleteEntityQuery
            {
                Locator = Actor,
            }.Run(world);
        }
    }
}