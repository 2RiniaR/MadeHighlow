using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Players.Cards
{
    public record DeleteCardQuery
    {
        [NotNull] public CardLocator Locator { get; init; } = new();

        [NotNull]
        public World Run([NotNull] in World world)
        {
            throw new NotImplementedException();
        }
    }
}