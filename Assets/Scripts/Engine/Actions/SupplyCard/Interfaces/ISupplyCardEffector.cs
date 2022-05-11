using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface ISupplyCardEffector
    {
        public ValueList<Interrupt<SupplyCardEffect>> EffectsOnSupplyCard(
            [NotNull] IActionContext context,
            [NotNull] SupplyCardAction action
        );
    }
}
