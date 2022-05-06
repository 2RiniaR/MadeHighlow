using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record SucceedAddComponentResult([NotNull] in Component AddedComponent) : AddComponentResult
    {
        public override World Simulate(in World world)
        {
            return AddedComponent.CreateIn(world);
        }
    }
}