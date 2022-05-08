using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     プレイヤー
    /// </summary>
    public record Player(
        ID ID,
        [NotNull] [ItemNotNull] ValueList<Card> Cards,
        [NotNull] DeckSize DeckSize,
        [NotNull] [ItemNotNull] ValueList<Component> Components
    ) : IIdentified, IAttachable
    {
        public PlayerID PlayerID => new(ID);

        IAttachableID IAttachable.AttachableID => PlayerID;

        public IAttachable WithComponents(ValueList<Component> components)
        {
            return this with { Components = components };
        }

        public World UpdateIn(World world)
        {
            return world with { Players = world.Players.ReplaceItem(player => player.PlayerID == PlayerID, this) };
        }

        [NotNull]
        public World CreateIn([NotNull] World world)
        {
            return world with { Players = world.Players.Add(this) };
        }

        [NotNull]
        public static ValueList<Player> GetAllFrom([NotNull] World world)
        {
            return world.Players;
        }

        [NotNull]
        [ItemNotNull]
        public ValueList<IObject> GetChildren()
        {
            return ValueList.Concat(
                Components.Select(item => item as IObject),
                Components.SelectMany(item => item.GetChildren()),
                Cards.Select(item => item as IObject),
                Cards.SelectMany(item => item.GetChildren())
            );
        }
    }
}
