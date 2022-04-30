using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Players
{
    public record GetPlayerQuery
    {
        [NotNull] public PlayerLocator Locator { get; init; } = new();

        [NotNull]
        public Player Run([NotNull] in World world)
        {
            return world.Players.Items.Find(player => player.ID == Locator.PlayerID) ??
                   throw new NullReferenceException();
        }
    }
}