using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.UnregisterCard;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteCard
{
    public record UnregisterCardFailedResult(
        [NotNull] DeleteCardAction Action,
        [NotNull] [ItemNotNull] ValueList<Event<DeleteComponent.SucceedResult>> DeleteComponentEvents,
        [NotNull] UnregisterCardResult Failed
    ) : DeleteCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
