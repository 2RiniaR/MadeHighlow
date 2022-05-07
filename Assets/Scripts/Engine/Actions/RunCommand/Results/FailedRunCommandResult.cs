namespace RineaR.MadeHighlow
{
    public record FailedRunCommandResult(FailedRunCommandReason Reason) : RunCommandResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}