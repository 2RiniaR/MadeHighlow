using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.SupplyCard
{
    public interface ISupplyCardEffector
    {
        public ValueList<Interrupt<SupplyCardEffect>> EffectsOnSupplyCard(
            [NotNull] IHistory history,
            [NotNull] Card card
        );
    }
}
