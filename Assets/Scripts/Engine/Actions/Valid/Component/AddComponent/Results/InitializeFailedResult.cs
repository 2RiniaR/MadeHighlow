using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.AddComponent
{
    public record InitializeFailedResult(
        [NotNull] IAttachableID TargetID,
        [NotNull] Component InitialStatus,
        [NotNull] Fragment.RegisterComponent.SucceedResult RegisterComponentResult,
        [NotNull] ValueList<ReactedResult> SucceedResults,
        [NotNull] ReactedResult FailedResult
    ) : AddComponentResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
