using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Players.Cards
{
    public record GetCardQuery
    {
        [NotNull] public CardLocator Locator { get; init; } = new();

        [NotNull]
        public Card Run([NotNull] in World world)
        {
            return new GetPlayerQuery { Locator = Locator }.Run(world)
                .Cards.Items.Find(card => card.ID == Locator.CardID) ?? throw new NullReferenceException();
        }
    }
}