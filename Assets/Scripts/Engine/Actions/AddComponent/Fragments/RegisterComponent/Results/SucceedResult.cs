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
            var currentWorld = world;
            currentWorld = AllocateIDResult.Simulate(currentWorld);
            currentWorld = Registered.CreateIn(currentWorld);
            return currentWorld;
        }
    }
}
