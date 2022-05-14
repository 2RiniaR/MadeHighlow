using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カード
    /// </summary>
    public abstract record Card(
        ID ID,
        [NotNull] PlayerID OwnerPlayerID,
        CardGenre Genre,
        Quickness Quickness,
        [NotNull] [ItemNotNull] ValueList<Component> Components
    ) : IIdentified, IAttachable
    {
        public CardID CardID => new(ID);

        public IAttachable WithComponents(ValueList<Component> components)
        {
            return this with { Components = components };
        }

        IAttachableID IAttachable.AttachableID => CardID;

        public World UpdateIn(World world)
        {
            var player = OwnerPlayerID.GetFrom(world) ?? throw new NullReferenceException();
            var modifiedPlayer = player with
            {
                Cards = player.Cards.ReplaceItem(card => card.CardID == CardID, this),
            };
            return modifiedPlayer.UpdateIn(world);
        }

        [NotNull]
        public World CreateIn([NotNull] World world)
        {
            var player = OwnerPlayerID.GetFrom(world) ?? throw new NullReferenceException();
            var modifiedPlayer = player with { Cards = player.Cards.Add(this) };
            return modifiedPlayer.UpdateIn(world);
        }

        [NotNull]
        public World DeleteFrom([NotNull] World world)
        {
            throw new NotImplementedException();
        }

        [NotNull]
        public static ValueList<Card> GetAllFrom([NotNull] World world)
        {
            return Player.GetAllFrom(world).SelectMany(player => player.Cards);
        }

        [NotNull]
        [ItemNotNull]
        public ValueList<IObject> GetChildren()
        {
            return ValueList.Concat(
                Components.Select(item => item as IObject),
                Components.SelectMany(item => item.GetChildren())
            );
        }
    }
}
