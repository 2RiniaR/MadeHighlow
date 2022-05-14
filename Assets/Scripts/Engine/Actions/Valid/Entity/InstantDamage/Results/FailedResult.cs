namespace RineaR.MadeHighlow.Actions.Valid.InstantDamage
{
    public record FailedResult(FailedReason Reason) : InstantDamageResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
