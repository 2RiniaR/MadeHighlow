using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantHeal
{
    public interface IInstantHealRejector : IPriority<IInstantHealRejector>
    {
        [CanBeNull]
        public Interrupt<InstantHealRejection> InstantHealRejection(
            [NotNull] IHistory history,
            [NotNull] InstantHealAction action,
            [NotNull] [ItemNotNull] ValueList<Interrupt<InstantHealRejection>> collected
        );
    }
}
