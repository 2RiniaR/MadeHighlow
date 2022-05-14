namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public record FailedResult(FailedReason Reason) : InstantDamageResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
