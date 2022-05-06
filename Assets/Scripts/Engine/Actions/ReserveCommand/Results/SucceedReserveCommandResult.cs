namespace RineaR.MadeHighlow
{
    public record SucceedReserveCommandResult<TOption>(Command<TOption> Command) : ReserveCommandResult
    {
        public override World Simulate(in World world)
        {
            return world with
            {
                ReservedCommands = world.ReservedCommands.Add(Command),
            };
        }
    }
}