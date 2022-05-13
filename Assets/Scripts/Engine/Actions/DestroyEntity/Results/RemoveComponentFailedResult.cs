using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.RemoveComponent;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public record RemoveComponentFailedResult(
        [NotNull] Entity Target,
        [NotNull] [ItemNotNull] ValueList<Interrupt<DestroyEntityEffect>> Interrupts,
        [NotNull] ValueList<RemoveComponent.SucceedResult> SucceedResults,
        RemoveComponentResult FailedResult
    ) : DestroyEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
