using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity.RegisterEntity
{
    public record RegisterEntityResult(
        [NotNull] AllocateIDResult AllocateIDResult,
        [NotNull] Entity Registered
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
