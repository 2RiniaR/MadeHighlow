using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PlaceCard
{
    public interface IReplacer : IPriority<IReplacer>
    {
        [ItemNotNull]
        [CanBeNull]
        public ValueList<Interrupt<CardReplacement>> CardReplacements(
            [NotNull] IHistory history,
            [NotNull] Action action,
            [NotNull] [ItemNotNull] ValueList<Interrupt<CardReplacement>> collected
        );
    }
}
