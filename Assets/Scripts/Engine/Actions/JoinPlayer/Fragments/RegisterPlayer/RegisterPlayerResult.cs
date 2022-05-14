using JetBrains.Annotations;
using RineaR.MadeHighlow.FragmentActions;

namespace RineaR.MadeHighlow.Actions.JoinPlayer.RegisterPlayer
{
    public record RegisterPlayerResult(
        [NotNull] AllocateIDResult AllocateIDResult,
        [NotNull] Player Registered
    ) : Result
    {
        public override World Simulate(World world)
        {
            world = AllocateIDResult.Simulate(world);
            world = Registered.CreateIn(world);
            return world;
        }
    }
}
