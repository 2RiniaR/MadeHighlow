using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Cards;

namespace RineaR.MadeHighlow.Engine.Queries.Players.Cards
{
    public record GetAllCardsQuery
    {
        [NotNull]
        public ImmutableList<Card> Run([NotNull] in World world)
        {
            return world.Players.SelectMany(player => player.Cards).ToImmutableList();
        }
    }
}