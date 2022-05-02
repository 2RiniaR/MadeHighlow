using JetBrains.Annotations;
using RineaR.MadeHighlow.Queries.Objects;

namespace RineaR.MadeHighlow.Actions
{
    public record DestroyResult() : Result(ActionType.Destroy)
    {
        [NotNull] public ObjectLocator Actor { get; init; } = new();

        public override World Simulate(in World world)
        {
            return new DeleteObjectQuery
            {
                Locator = Actor,
            }.Run(world);
        }
    }
}