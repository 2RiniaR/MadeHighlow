using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DropCard
{
    public interface IDropCardEffector : IComponent
    {
        public ValueList<Interrupt<DropCardEffect>> EffectsOnDropCard(
            [NotNull] IHistory history,
            [NotNull] Card target
        );
    }
}
