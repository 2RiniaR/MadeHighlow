using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.PutCard
{
    public interface IPutCardEffector
    {
        public ValueList<Interrupt<PutCardEffect>> EffectsOnPutCard(
            [NotNull] IHistory history,
            [NotNull] Player player,
            [NotNull] Card card
        );
    }
}
