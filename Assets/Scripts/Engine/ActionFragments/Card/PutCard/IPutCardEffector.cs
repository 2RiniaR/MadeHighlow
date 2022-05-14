using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow.ActionFragments.PutCard
{
    public interface IPutCardEffector
    {
        public ValueList<Interrupt<PutCardEffect>> EffectsOnPutCard(
            [NotNull] IHistory context,
            [NotNull] Player player,
            [NotNull] Card card
        );
    }
}
