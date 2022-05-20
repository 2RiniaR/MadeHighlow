using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityFly
{
    public interface IEntityFlyRejector : IPriority<IEntityFlyRejector>
    {
        [NotNull]
        public Interrupt<EntityFlyRejection> EntityFlyRejection(
            [NotNull] IHistory history,
            [NotNull] EntityFlyAction action,
            [NotNull] EntityFlyProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<EntityFlyRejection>> collected
        );
    }
}
