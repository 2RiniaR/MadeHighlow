using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public interface ISupplyCardEffector
    {
        public ValueList<Interrupt<SupplyCardEffect>> EffectsOnSupplyCard(
            [NotNull] IActionContext context,
            [NotNull] Card card
        );
    }
}
