using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IWorldModifier
    {
        [NotNull]
        World UpdateCard([NotNull] World world, [NotNull] Card after);

        [NotNull]
        World CreateCard([NotNull] World world, [NotNull] Card after);

        [NotNull]
        World DeleteCard([NotNull] World world, [NotNull] CardID id);

        [NotNull]
        World UpdateEntity([NotNull] World world, [NotNull] Entity after);

        [NotNull]
        World CreateEntity([NotNull] World world, [NotNull] Entity after);

        [NotNull]
        World DeleteEntity([NotNull] World world, [NotNull] EntityID id);

        [NotNull]
        World UpdateComponent([NotNull] World world, [NotNull] Component after);

        [NotNull]
        World CreateComponent([NotNull] World world, [NotNull] Component after);

        [NotNull]
        World DeleteComponent([NotNull] World world, [NotNull] ComponentID id);

        [NotNull]
        World UpdateTile([NotNull] World world, [NotNull] Tile after);

        [NotNull]
        World CreateTile([NotNull] World world, [NotNull] Tile after);

        [NotNull]
        World DeleteTile([NotNull] World world, [NotNull] TileID id);

        [NotNull]
        World UpdatePlayer([NotNull] World world, [NotNull] Player after);

        [NotNull]
        World CreatePlayer([NotNull] World world, [NotNull] Player after);

        [NotNull]
        World DeletePlayer([NotNull] World world, [NotNull] PlayerID id);

        [NotNull]
        World UpdateUnit([NotNull] World world, [NotNull] Unit after);

        [NotNull]
        World CreateUnit([NotNull] World world, [NotNull] Unit after);

        [NotNull]
        World DeleteUnit([NotNull] World world, [NotNull] UnitID id);

        [NotNull]
        World UpdateAttachable([NotNull] World world, [NotNull] IAttachable after);
    }
}
