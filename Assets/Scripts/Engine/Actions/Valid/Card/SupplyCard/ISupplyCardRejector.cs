using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.SupplyCard
{
    public interface ISupplyCardRejector : IPriority<ISupplyCardRejector>
    {
        [NotNull]
        [ItemNotNull]
        public Interrupt<SupplyCardRejection> SupplyCardRejection(
            [NotNull] IHistory history,
            [NotNull] SupplyCardAction action,
            [NotNull] SupplyCardProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<SupplyCardRejection>> collected
        );
    }
}
