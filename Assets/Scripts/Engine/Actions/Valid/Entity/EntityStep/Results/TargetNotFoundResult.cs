using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid
{
    public record TargetNotFoundResult([NotNull] EntityID TargetID) : EntityStepResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
