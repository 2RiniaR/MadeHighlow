using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity.RegisterEntity
{
    public record SucceedResult(
        [NotNull] AllocateIDResult AllocateIDResult,
        [NotNull] Entity Registered
    ) : RegisterEntityResult
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
