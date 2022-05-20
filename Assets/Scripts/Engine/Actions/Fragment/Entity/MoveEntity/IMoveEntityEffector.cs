using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.MoveEntity
{
    public interface IMoveEntityEffector
    {
        [NotNull]
        [ItemNotNull]
        public ValueList<Interrupt<MoveEntityEffect>> EffectsOnMoveEntity(
            [NotNull] IHistory history,
            [NotNull] MoveEntityAction action,
            [NotNull] Process process
        );
    }
}
