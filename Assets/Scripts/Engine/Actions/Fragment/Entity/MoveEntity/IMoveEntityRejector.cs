using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.MoveEntity
{
    public interface IMoveEntityRejector : IPriority<IMoveEntityRejector>
    {
        [NotNull]
        public Interrupt<MoveEntityRejection> MoveEntityRejection(
            [NotNull] IHistory history,
            [NotNull] MoveEntityAction action,
            [NotNull] MoveEntityProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<MoveEntityRejection>> collected
        );
    }
}
