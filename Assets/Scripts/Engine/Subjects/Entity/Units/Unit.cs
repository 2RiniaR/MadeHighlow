using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ユニット
    /// </summary>
    public sealed record Unit(
        in ID ID,
        [NotNull] in Position3D Position3D,
        [NotNull] in Direction3D Direction3D,
        [CanBeNull] in Vitality Vitality,
        [NotNull] [ItemNotNull] in ValueObjectList<Component> Components,
        [NotNull] in Strength Strength,
        [NotNull] in Medo Medo,
        in Shadow Shadow,
        [NotNull] in Figure Figure,
        [NotNull] in PlayerID FollowingPlayerID
    ) : Entity(ID, Position3D, Direction3D, Vitality, Components)
    {
        public UnitID UnitID => new(ID);

        [NotNull]
        [ItemNotNull]
        public static ValueObjectList<Unit> All([NotNull] in World world)
        {
            return world.Entities.WhereType<Unit>();
        }
    }
}