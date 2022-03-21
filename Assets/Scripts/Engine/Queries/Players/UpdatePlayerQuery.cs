using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Players;

namespace RineaR.MadeHighlow.Engine.Queries.Players
{
    public record UpdatePlayerQuery
    {
        [NotNull] public PlayerLocator Locator { get; init; } = new();
        [NotNull] public Player Value { get; init; } = new();

        [NotNull]
        public World Run([NotNull] in World world)
        {
            return world with
            {
                Players = world.Players.ReplaceItem(player => player.ID == Locator.PlayerID, Value),
            };
        }
    }
}