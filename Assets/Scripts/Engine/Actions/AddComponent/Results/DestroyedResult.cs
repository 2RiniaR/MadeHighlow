using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public record DestroyedResult(
        [NotNull] IAttachableID TargetID,
        [NotNull] Component InitialStatus,
        [NotNull] RegisterComponent.SucceedResult RegisterComponentResult,
        [NotNull] ValueList<Result> InitializeResults
    ) : AddComponentResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
