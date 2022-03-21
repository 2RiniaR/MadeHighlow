using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Exceptions;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Cards;

namespace RineaR.MadeHighlow.Engine.Queries.Players.Cards
{
    public record GetCardQuery
    {
        [NotNull] public CardLocator Locator { get; init; } = new();

        [NotNull]
        public Card Run([NotNull] in World world)
        {
            return new GetPlayerQuery { Locator = Locator }.Run(world)
                .Cards.Find(card => card.ID == Locator.CardID) ?? throw new NotExistException();
        }
    }
}