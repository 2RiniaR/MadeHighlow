namespace RineaR.MadeHighlow
{
    public record FailedRunCommandResult : RunCommandResult
    {
        public FailedRunCommandReason Reason { get; init; }

        public override World Simulate(in World world)
        {
            return world;
        }
    }
}