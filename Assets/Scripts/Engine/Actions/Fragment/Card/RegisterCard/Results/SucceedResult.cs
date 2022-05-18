using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.RegisterCard
{
    public record SucceedResult([NotNull] RegisterCardAction Action, [NotNull] Card Registered) : RegisterCardResult
    {
        public override World Simulate(World world)
        {
            return Registered.CreateIn(world);
        }
    }
}
