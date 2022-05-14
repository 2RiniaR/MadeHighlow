using JetBrains.Annotations;

namespace RineaR.MadeHighlow.ActionFragments.RegisterCard
{
    public record SucceedResult(
        [NotNull] AllocateIDResult AllocateIDResult,
        [NotNull] Card Registered
    ) : RegisterCardResult
    {
        public override World Simulate(World world)
        {
            world = AllocateIDResult.Simulate(world);
            world = Registered.CreateIn(world);
            return world;
        }
    }
}
