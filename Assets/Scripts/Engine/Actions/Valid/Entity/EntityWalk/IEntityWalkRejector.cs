using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityWalk
{
    public interface IEntityWalkRejector : IPriority<IEntityWalkRejector>
    {
        [CanBeNull]
        public Interrupt<EntityWalkRejection> EntityWalkRejection(
            [NotNull] IHistory history,
            [NotNull] EntityWalkAction action,
            [NotNull] EntityWalkProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<EntityWalkRejection>> collected
        );
    }
}
