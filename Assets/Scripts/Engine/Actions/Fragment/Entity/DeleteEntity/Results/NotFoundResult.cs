using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteEntity
{
    public record NotFoundResult([NotNull] DeleteEntityAction Action) : DeleteEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
