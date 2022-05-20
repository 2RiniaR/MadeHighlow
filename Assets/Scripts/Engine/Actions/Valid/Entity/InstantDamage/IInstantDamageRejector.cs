using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantDamage
{
    public interface IInstantDamageRejector : IPriority<IInstantDamageRejector>
    {
        [NotNull]
        public Interrupt<InstantDamageRejection> InstantDamageRejection(
            [NotNull] IHistory history,
            [NotNull] InstantDamageAction action,
            [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDamageRejection>> collected
        );
    }
}
