namespace RineaR.MadeHighlow
{
    public record CanceledRunCommandResult(Command TriedCommand) : RunCommandResult
    {
        public override World Simulate(in World world)
        {
            return world;
        }
    }
}