using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityWalk
{
    public record SucceedResult(
        [NotNull] EntityWalkAction Action,
        [NotNull] EntityWalkProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityWalkRejection>> Interrupts
    ) : EntityWalkResult;
}
