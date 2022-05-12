using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.RemoveComponent;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public record RemoveComponentFailedResult(
        [NotNull] Entity Entity,
        [NotNull] ValueList<RemoveComponent.SucceedResult> Succeeds,
        RemoveComponentResult Failed
    ) : DestroyEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
