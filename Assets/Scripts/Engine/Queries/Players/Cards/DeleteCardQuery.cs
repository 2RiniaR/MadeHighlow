using System;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Cards;

namespace RineaR.MadeHighlow.Engine.Queries.Players.Cards
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