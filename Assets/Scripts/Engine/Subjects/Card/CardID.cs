using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record CardID(ID Content) : IAttachableID
    {
        IAttachable IAttachableID.GetFrom(World world)
        {
            return GetFrom(world);
        }

        [CanBeNull]
        public Card GetFrom([NotNull] World world)
        {
            return Card.GetAllFrom(world).Find(card => card.CardID == this);
        }
    }
}
