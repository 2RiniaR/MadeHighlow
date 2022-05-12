using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public record SucceedResult([NotNull] Component AddedComponent) : AddComponentResult
    {
        public override World Simulate(World world)
        {
            return AddedComponent.CreateIn(world);
        }
    }
}
