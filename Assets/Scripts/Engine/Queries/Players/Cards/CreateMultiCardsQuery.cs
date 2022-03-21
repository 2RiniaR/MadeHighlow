using System.Collections.Immutable;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Cards;
using RineaR.MadeHighlow.Engine.Subjects.Players;

namespace RineaR.MadeHighlow.Engine.Queries.Players.Cards
{
    public record CreateMultiCardsQuery
    {
        [NotNull] public PlayerLocator ParentLocator { get; init; } = new();
        [NotNull] [ItemNotNull] public ImmutableList<Card> Values { get; init; } = ImmutableList<Card>.Empty;

        [NotNull]
        public World Run([NotNull] in World world)
        {
            var player = new GetPlayerQuery { Locator = ParentLocator }.Run(world);
            return new UpdatePlayerQuery
            {
                Locator = ParentLocator,
                Value = player with
                {
                    Cards = player.Cards.AddRange(Values),
                },
            }.Run(world);
        }
    }
}