namespace RineaR.MadeHighlow
{
    public record FailedRunCommandResult(in FailedRunCommandReason Reason) : RunCommandResult
    {
        public override World Simulate(in World world)
        {
            return world;
        }
    }
}