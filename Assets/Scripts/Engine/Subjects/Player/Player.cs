using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「プレイヤー」を表現する
    /// </summary>
    public record Player : IIdentified, IAttachable
    {
        /// <summary>
        ///     セッション内での識別子
        /// </summary>
        public ID ID { get; init; } = ID.None;

        /// <summary>
        ///     デッキにあるカード
        /// </summary>
        [NotNull]
        public ValueObjectList<Card> Cards { get; init; } = ValueObjectList<Card>.Empty;

        /// <summary>
        ///     デッキの大きさ
        /// </summary>
        [NotNull]
        public PlayerDeckSize DeckSize { get; init; } = new();

        /// <summary>
        ///     コンポーネント
        /// </summary>
        public ValueObjectList<Component> Components { get; init; } = ValueObjectList<Component>.Empty;

        public PlayerEnsuredID EnsuredID => new() { Content = ID };

        IAttachableEnsuredID IAttachable.EnsuredID => EnsuredID;

        public IAttachable WithComponents(ValueObjectList<Component> components)
        {
            return this with { Components = components };
        }

        public World UpdateIn(in World world)
        {
            return world with { Players = world.Players.ReplaceItem(player => player.EnsuredID == EnsuredID, this) };
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