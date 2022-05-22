using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.DeleteComponent;

namespace RineaR.MadeHighlow.Actions.RemoveComponent
{
    public record DeleteComponentFailedResult(
        [NotNull] RemoveComponentAction Action,
        [NotNull] DeleteComponentResult Failed
    ) : RemoveComponentResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
