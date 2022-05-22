using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public interface IInstantDamageRejector : IPriority<IInstantDamageRejector>
    {
        [CanBeNull]
        public Interrupt<InstantDamageRejection> InstantDamageRejection(
            [NotNull] IHistory history,
            [NotNull] InstantDamageAction action,
            [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDamageRejection>> collected
        );
    }
}
