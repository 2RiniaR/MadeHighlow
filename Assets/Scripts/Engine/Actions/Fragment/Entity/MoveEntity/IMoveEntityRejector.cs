using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.MoveEntity
{
    public interface IMoveEntityRejector : IPriority<IMoveEntityRejector>
    {
        [CanBeNull]
        public Interrupt<MoveEntityRejection> MoveEntityRejection(
            [NotNull] IHistory history,
            [NotNull] MoveEntityAction action,
            [NotNull] MoveEntityProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<MoveEntityRejection>> collected
        );
    }
}
