using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.DropCard
{
    public interface IDropCardRejector : IPriority<IDropCardRejector>
    {
        [CanBeNull]
        public Interrupt<DropCardRejection> DropCardRejection(
            [NotNull] IHistory history,
            [NotNull] DropCardAction action,
            [NotNull] DropCardProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<DropCardRejection>> collected
        );
    }
}
