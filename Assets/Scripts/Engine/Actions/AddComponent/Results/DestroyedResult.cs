using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public record DestroyedResult(
        [NotNull] IAttachableID TargetID,
        [NotNull] Component InitialStatus,
        [NotNull] ActionFragments.RegisterComponent.SucceedResult RegisterComponentResult,
        [NotNull] ValueList<ReactedResult> InitializeResults
    ) : AddComponentResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
