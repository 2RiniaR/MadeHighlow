using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.SupplyCard
{
    public interface ISupplyCardEffector
    {
        [NotNull]
        [ItemNotNull]
        public ValueList<Interrupt<SupplyCardEffect>> EffectsOnSupplyCard(
            [NotNull] IHistory history,
            [NotNull] Process process
        );
    }
}
