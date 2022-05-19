using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.DeleteComponent;

namespace RineaR.MadeHighlow.Actions.Valid.RemoveComponent
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
