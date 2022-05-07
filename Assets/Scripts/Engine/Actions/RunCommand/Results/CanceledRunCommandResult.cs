namespace RineaR.MadeHighlow
{
    public record CanceledRunCommandResult(Command TriedCommand) : RunCommandResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}