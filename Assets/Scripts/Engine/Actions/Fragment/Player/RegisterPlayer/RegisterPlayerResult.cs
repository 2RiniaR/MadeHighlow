using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.RegisterPlayer
{
    public record RegisterPlayerResult([NotNull] RegisterPlayerAction Action, [NotNull] Player Registered) : Result
    {
        public override World Simulate(World world)
        {
            return Registered.CreateIn(world);
        }
    }
}
