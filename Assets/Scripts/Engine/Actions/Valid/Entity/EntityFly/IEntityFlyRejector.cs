using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public interface IEntityFlyRejector : IPriority<IEntityFlyRejector>
    {
        [CanBeNull]
        public Interrupt<EntityFlyRejection> EntityFlyRejection(
            [NotNull] IHistory history,
            [NotNull] EntityFlyAction action,
            [NotNull] EntityFlyProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<EntityFlyRejection>> collected
        );
    }
}
