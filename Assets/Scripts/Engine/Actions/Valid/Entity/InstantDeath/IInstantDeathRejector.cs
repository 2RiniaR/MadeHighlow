using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDeath
{
    public interface IInstantDeathRejector : IPriority<IInstantDeathRejector>
    {
        [CanBeNull]
        public Interrupt<InstantDeathRejection> InstantDeathRejection(
            [NotNull] IHistory history,
            [NotNull] InstantDeathAction action,
            [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDeathRejection>> collected
        );
    }
}
