namespace RineaR.MadeHighlow
{
    public record FailedSupplyCardResult : SupplyCardResult
    {
        public override World Simulate(in World world)
        {
            return world;
        }
    }
}