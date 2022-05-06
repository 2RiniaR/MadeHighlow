namespace RineaR.MadeHighlow
{
    public record FailedRunOperationResult : RunOperationResult
    {
        public FailedRunOperationReason Reason { get; init; }

        public override World Simulate(in World world)
        {
            return world;
        }
    }
}