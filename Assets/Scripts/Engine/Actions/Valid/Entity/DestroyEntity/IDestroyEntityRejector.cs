using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public interface IDestroyEntityRejector : IPriority<IDestroyEntityRejector>
    {
        [CanBeNull]
        public Interrupt<DestroyEntityRejection> DestroyEntityRejection(
            [NotNull] IHistory history,
            [NotNull] DestroyEntityAction action,
            [NotNull] DestroyEntityProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<DestroyEntityRejection>> collected
        );
    }
}
