using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.DropCard
{
    public interface IDropCardEffector : IPriority<IDropCardEffector>
    {
        [NotNull]
        [ItemNotNull]
        public ValueList<Interrupt<DropCardEffect>> EffectsOnDropCard(
            [NotNull] IHistory history,
            [NotNull] DropCardAction action,
            [NotNull] DropCardProcess process
        );
    }
}
