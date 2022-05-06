using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     「カード」
    /// </summary>
    public abstract record Card : IIdentified, IAttachable
    {
        /// <summary>
        ///     セッション内での識別子
        /// </summary>
        public ID ID { get; init; } = ID.None;

        /// <summary>
        ///     所持しているプレイヤーのID
        /// </summary>
        public PlayerEnsuredID PlayerID { get; init; } = new();

        /// <summary>
        ///     種類
        /// </summary>
        public CardGenre Genre { get; init; } = CardGenre.Common;

        /// <summary>
        ///     機能
        /// </summary>
        [NotNull]
        public Command Command { get; init; } = Command.Empty;

        /// <summary>
        ///     コンポーネント
        /// </summary>
        public ValueObjectList<Component> Components { get; init; } = ValueObjectList<Component>.Empty;

        public CardEnsuredID EnsuredID => new() { Content = ID };

        public IAttachable WithComponents(ValueObjectList<Component> components)
        {
            return this with { Components = components };
        }

        IAttachableEnsuredID IAttachable.EnsuredID => EnsuredID;

        public World UpdateIn(in World world)
        {
            var player = PlayerID.GetFrom(world) ?? throw new NullReferenceException();
            var modifiedPlayer = player with
            {
                Cards = player.Cards.ReplaceItem(card => card.EnsuredID == EnsuredID, this),
            };
            return modifiedPlayer.UpdateIn(world);
        }

        [NotNull]
        public World CreateIn([NotNull] in World world)
        {
            var player = PlayerID.GetFrom(world) ?? throw new NullReferenceException();
            var modifiedPlayer = player with { Cards = player.Cards.Add(this) };
            return modifiedPlayer.UpdateIn(world);
        }

        [NotNull]
        public static ValueObjectList<Card> GetAllFrom([NotNull] in World world)
        {
            return Player.GetAllFrom(world).SelectMany(player => player.Cards);
        }

        [NotNull]
        [ItemNotNull]
        public ValueObjectList<IObject> GetChildren()
        {
            return ValueObjectList.Concat(
                Components.Select(item => item as IObject),
                Components.SelectMany(item => item.GetChildren())
            );
        }
    }

    public sealed record Card<TCommand> : Card where TCommand : Command<TCommand>
    {
        public new CardEnsuredID<TCommand> EnsuredID => new() { Content = ID };

        /// <summary>
        ///     機能
        /// </summary>
        [NotNull]
        public new Command<TCommand> Command { get; init; } = Command<TCommand>.Empty;
    }
}