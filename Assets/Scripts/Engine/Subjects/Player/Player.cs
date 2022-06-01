using System;
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
