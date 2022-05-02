using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries
{
    public record GetPlayerQuery
    {
        [NotNull] public PlayerLocator Locator { get; init; } = new();

        [NotNull]
        public Player Run([NotNull] in World world)
        {
            return world.Players.Find(player => player.ID == Locator.PlayerID) ?? throw new NullReferenceException();
        }
    }
}