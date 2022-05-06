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
        [NotNull]
        public PlayerID PlayerID { get; init; } = new();

        /// <summary>
        ///     種類
        /// </summary>
        public CardGenre Genre { get; init; } = CardGenre.Common;

        /// <summary>
        ///     命令の早さ
        /// </summary>
        public Quickness Quickness { get; init; } = Quickness.Last;

        /// <summary>
        ///     コンポーネント
        /// </summary>
        public ValueObjectList<Component> Components { get; init; } = ValueObjectList<Component>.Empty;

        public CardID EnsuredID => new() { Content = ID };

        public IAttachable WithComponents(ValueObjectList<Component> components)
        {
            return this with { Components = components };
        }

        IAttachableID IAttachable.EnsuredID => EnsuredID;

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

        private record EmptyImpl : Card;

        [NotNull] public static Card Empty => new EmptyImpl();
    }

    public abstract record Card<TOption> : Card
    {
        /// <summary>
        ///     指定された追加データから、アクションを生成する
        /// </summary>
        [NotNull]
        public abstract Action GenerateAction([NotNull] TOption option, [NotNull] UnitID unitID);

        private record EmptyImpl : Card<TOption>
        {
            public override Action GenerateAction(TOption option, UnitID unitID)
            {
                return Action.Empty;
            }
        }

        [NotNull] public new static Card<TOption> Empty => new EmptyImpl();
    }
}