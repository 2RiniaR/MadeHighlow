using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent.RegisterComponent
{
    public record SucceedResult(
        [NotNull] AllocateIDResult AllocateIDResult,
        [NotNull] Component Registered
    ) : RegisterComponentResult
    {
        public override World Simulate(World world)
        {
            world = AllocateIDResult.Simulate(world);
            world = Registered.CreateIn(world);
            return world;
        }
    }
}
