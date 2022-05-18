using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.PlaceCard
{
    public record RejectedResult(
        [NotNull] PlaceCardAction Action,
        [CanBeNull] [ItemNotNull] ValueList<Interrupt<PlaceCardReplaceEffect>> ReplaceInterrupts,
        [NotNull] Process Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<PlaceCardEffect>> Interrupts,
        [NotNull] ComponentID RejectedID
    ) : PlaceCardResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
