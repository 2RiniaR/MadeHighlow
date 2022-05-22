using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterComponent
{
    public record ParentNotFoundResult([NotNull] RegisterComponentAction Action) : RegisterComponentResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
