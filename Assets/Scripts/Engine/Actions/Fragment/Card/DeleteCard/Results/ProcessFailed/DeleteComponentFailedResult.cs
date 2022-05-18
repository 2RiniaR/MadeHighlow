using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.DeleteComponent;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteCard
{
    public record DeleteComponentFailedResult(
        [NotNull] DeleteCardAction Action,
        [NotNull] [ItemNotNull] ValueList<Event<DeleteComponent.SucceedResult>> DeleteComponentEvents,
        [NotNull] DeleteComponentResult Failed
    ) : DeleteCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
