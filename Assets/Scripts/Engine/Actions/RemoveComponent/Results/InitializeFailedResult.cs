using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RemoveComponent
{
    public record FinalizeFailedResult(
        [NotNull] Component Target,
        [NotNull] ValueList<ReactedResult> SucceedResults,
        [NotNull] ReactedResult FailedResult
    ) : RemoveComponentResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
