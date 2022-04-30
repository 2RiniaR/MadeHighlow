using System.Linq;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Players.Cards
{
    public record GetAllCardsQuery
    {
        [NotNull]
        public ValueObjectList<Card> Run([NotNull] in World world)
        {
            return world.Players.Items.SelectMany(player => player.Cards.Items).ToValueObjectList();
        }
    }
}