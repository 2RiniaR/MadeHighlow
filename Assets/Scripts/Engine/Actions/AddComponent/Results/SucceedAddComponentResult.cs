using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record SucceedAddComponentResult([NotNull] Component AddedComponent) : AddComponentResult
    {
        public override World Simulate(World world)
        {
            return AddedComponent.CreateIn(world);
        }
    }
}