using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     プレイヤー
    /// </summary>
    public record Player(
        in ID ID,
        [NotNull] [ItemNotNull] in ValueObjectList<Card> Cards,
        [NotNull] in DeckSize DeckSize,
        [NotNull] [ItemNotNull] in ValueObjectList<Component> Components
    ) : IIdentified, IAttachable
    {
        public PlayerID PlayerID => new(ID);

        IAttachableID IAttachable.AttachableID => PlayerID;

        public IAttachable WithComponents(ValueObjectList<Component> components)
        {
            return this with { Components = components };
        }

        public World UpdateIn(in World world)
        {
            return world with { Players = world.Players.ReplaceItem(player => player.PlayerID == PlayerID, this) };
        }

        [NotNull]
        public World CreateIn([NotNull] in World world)
        {
            return world with { Players = world.Players.Add(this) };
        }

        [NotNull]
        public static ValueObjectList<Player> GetAllFrom([NotNull] in World world)
        {
            return world.Players;
        }

        [NotNull]
        [ItemNotNull]
        public ValueObjectList<IObject> GetChildren()
        {
            return ValueObjectList.Concat(
                Components.Select(item => item as IObject),
                Components.SelectMany(item => item.GetChildren()),
                Cards.Select(item => item as IObject),
                Cards.SelectMany(item => item.GetChildren())
            );
        }
    }
}