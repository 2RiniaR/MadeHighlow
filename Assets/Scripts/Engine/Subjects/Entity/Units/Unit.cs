using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ユニット
    /// </summary>
    public sealed record Unit(
        ID ID,
        [NotNull] Position3D Position3D,
        [NotNull] Direction3D Direction3D,
        [CanBeNull] Vitality Vitality,
        bool FollowGravity,
        [NotNull] [ItemNotNull] ValueList<Component> Components,
        [NotNull] Strength Strength,
        [NotNull] Medo Medo,
        Shadow Shadow,
        [NotNull] Figure Figure,
        [NotNull] PlayerID FollowingPlayerID
    ) : Entity(ID, Position3D, Direction3D, Vitality, FollowGravity, Components)
    {
        public UnitID UnitID => new(ID);

        [NotNull]
        [ItemNotNull]
        public static ValueList<Unit> All([NotNull] World world)
        {
            return world.Entities.WhereType<Unit>();
        }
    }
}
