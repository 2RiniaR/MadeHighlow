using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record CanceledRunOperationResult : RunOperationResult
    {
        [NotNull] public RunOperationAction TriedAction { get; init; } = new();

        public override World Simulate(in World world)
        {
            return world;
        }
    }
}