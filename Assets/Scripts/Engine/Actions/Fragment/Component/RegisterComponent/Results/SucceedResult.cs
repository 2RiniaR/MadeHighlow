using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.RegisterComponent
{
    public record SucceedResult
        ([NotNull] RegisterComponentAction Action, [NotNull] Component Registered) : RegisterComponentResult
    {
        public override World Simulate(World world)
        {
            return Registered.CreateIn(world);
        }
    }
}
