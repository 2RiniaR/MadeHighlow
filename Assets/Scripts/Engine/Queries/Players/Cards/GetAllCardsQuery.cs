using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Cards
{
    public record GetAllCardsQuery
    {
        [NotNull]
        public ValueObjectList<Card> Run([NotNull] in World world)
        {
            return world.Players.SelectMany(player => player.Cards);
        }
    }
}