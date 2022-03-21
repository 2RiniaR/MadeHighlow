using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Exceptions;
using RineaR.MadeHighlow.Engine.Subjects;
using RineaR.MadeHighlow.Engine.Subjects.Players;

namespace RineaR.MadeHighlow.Engine.Queries.Players
{
    public record GetPlayerQuery
    {
        [NotNull] public PlayerLocator Locator { get; init; } = new();

        [NotNull]
        public Player Run([NotNull] in World world)
        {
            return world.Players.Find(player => player.ID == Locator.PlayerID) ?? throw new NotExistException();
        }
    }
}