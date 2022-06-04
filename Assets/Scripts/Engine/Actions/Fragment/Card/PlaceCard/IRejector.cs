using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PlaceCard
{
    public interface IRejector : IPriority<IRejector>
    {
        [CanBeNull]
        public Interrupt<RejectionContext> PlaceCardRejection(
            [NotNull] IHistory history,
            [NotNull] Action action,
            [NotNull] Process process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<RejectionContext>> collected
        );
    }
}
