using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RemoveComponent
{
    public record FinalizeFailedResult(
        [NotNull] Component Target,
        [NotNull] ValueList<Result> SucceedResults,
        [NotNull] Result FailedResult
    ) : RemoveComponentResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
