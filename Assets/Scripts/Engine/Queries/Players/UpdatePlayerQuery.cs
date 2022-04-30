using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Queries.Players
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
                Players = world.Players.Items.ReplaceItem(player => player.ID == Locator.PlayerID, Value)
                    .ToValueObjectList(),
            };
        }
    }
}