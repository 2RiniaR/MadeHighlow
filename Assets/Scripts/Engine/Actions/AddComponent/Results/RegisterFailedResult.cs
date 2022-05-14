using JetBrains.Annotations;
using RineaR.MadeHighlow.ActionFragments.RegisterComponent;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public record RegisterFailedResult(
        [NotNull] IAttachableID TargetID,
        [NotNull] Component InitialStatus,
        [NotNull] RegisterComponentResult FailedResult
    ) : AddComponentResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
