using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard.PutCard
{
    public interface IPutCardEffector
    {
        public ValueList<Interrupt<PutCardEffect>> EffectsOnPutCard(
            [NotNull] IActionContext context,
            [NotNull] Player player,
            [NotNull] Card card
        );
    }
}
