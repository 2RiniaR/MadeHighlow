using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid
{
    public record DestinationNotFoundResult([NotNull] Position2D Position) : EntityStepResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
