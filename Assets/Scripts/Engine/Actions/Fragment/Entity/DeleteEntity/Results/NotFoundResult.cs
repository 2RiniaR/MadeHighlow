using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteEntity
{
    public record NotFoundResult([NotNull] DeleteEntityAction Action) : DeleteEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
