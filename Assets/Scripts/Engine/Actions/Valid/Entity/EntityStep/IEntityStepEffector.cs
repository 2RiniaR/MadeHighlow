using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid
{
    public interface IEntityStepEffector
    {
        [NotNull]
        [ItemNotNull]
        public ValueList<Interrupt<EntityStepEffect>> EffectsOnEntityStep(
            [NotNull] IHistory session,
            [NotNull] Entity entity,
            [NotNull] [ItemNotNull] ValueList<Fragment.MoveEntity.SucceedResult> climbs,
            [NotNull] Fragment.MoveEntity.SucceedResult horizontal,
            [NotNull] [ItemNotNull] ValueList<Fragment.MoveEntity.SucceedResult> falls
        );
    }
}
