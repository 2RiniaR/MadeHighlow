using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.CreateComponent;

namespace RineaR.MadeHighlow.Actions.Valid.AddComponent
{
    public record CreateComponentFailedResult(
        [NotNull] AddComponentAction Action,
        [NotNull] CreateComponentResult Failed
    ) : AddComponentResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
