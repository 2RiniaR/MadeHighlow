using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.SupplyCard
{
    public interface ISupplyCardEffector : IPriority<ISupplyCardEffector>
    {
        [NotNull]
        [ItemNotNull]
        public ValueList<Interrupt<SupplyCardEffect>> EffectsOnSupplyCard(
            [NotNull] IHistory history,
            [NotNull] SupplyCardAction action,
            [NotNull] Process process
        );
    }
}
