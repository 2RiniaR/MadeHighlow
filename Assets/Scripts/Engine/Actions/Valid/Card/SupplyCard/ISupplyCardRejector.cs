using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public interface ISupplyCardRejector : IPriority<ISupplyCardRejector>
    {
        [CanBeNull]
        public Interrupt<SupplyCardRejection> SupplyCardRejection(
            [NotNull] IHistory history,
            [NotNull] SupplyCardAction action,
            [NotNull] SupplyCardProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<SupplyCardRejection>> collected
        );
    }
}
