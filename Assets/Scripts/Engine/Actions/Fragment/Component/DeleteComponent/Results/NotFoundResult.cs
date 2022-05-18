using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteComponent
{
    public record NotFoundResult([NotNull] DeleteComponentAction Action) : DeleteComponentResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
