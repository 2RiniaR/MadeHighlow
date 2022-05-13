using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.JoinPlayer.RegisterPlayer
{
    public record SucceedResult(
        [NotNull] AllocateIDResult AllocateIDResult,
        [NotNull] Player Registered
    ) : RegisterPlayerResult
    {
        public override World Simulate(World world)
        {
            var currentWorld = world;
            currentWorld = AllocateIDResult.Simulate(currentWorld);
            currentWorld = Registered.CreateIn(currentWorld);
            return currentWorld;
        }
    }
}
