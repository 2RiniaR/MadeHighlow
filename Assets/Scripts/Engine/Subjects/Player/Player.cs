using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record Player(
        ID ID,
        [NotNull] [ItemNotNull] ValueList<Card> Cards,
        [NotNull] DeckSize DeckSize,
        [NotNull] [ItemNotNull] ValueList<Component> Components,
        [NotNull] [ItemNotNull] ValueList<Unit> Units
    ) : IIdentified, IAttachable
    {
        public PlayerID PlayerID => new(ID);

        IAttachableID IAttachable.AttachableID => PlayerID;

        public IAttachable WithComponents(ValueList<Component> components)
        {
            return this with { Components = components };
        }
    }
}
