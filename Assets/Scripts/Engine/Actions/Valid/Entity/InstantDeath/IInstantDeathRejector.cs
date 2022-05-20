using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantDeath
{
    public interface IInstantDeathRejector : IPriority<IInstantDeathRejector>
    {
        [NotNull]
        public Interrupt<InstantDeathRejection> InstantDeathRejection(
            [NotNull] IHistory history,
            [NotNull] InstantDeathAction action,
            [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDeathRejection>> collected
        );
    }
}
