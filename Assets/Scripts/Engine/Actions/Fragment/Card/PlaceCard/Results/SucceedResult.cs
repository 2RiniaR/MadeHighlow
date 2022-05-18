using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.PlaceCard
{
    public record SucceedResult(
        [NotNull] PlaceCardAction Action,
        [CanBeNull] [ItemNotNull] ValueList<Interrupt<PlaceCardReplaceEffect>> ReplaceInterrupts,
        [NotNull] Process Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<PlaceCardEffect>> Interrupts
    ) : PlaceCardResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
