using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.RegisterComponent;

namespace RineaR.MadeHighlow.Actions.Valid.AddComponent
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
