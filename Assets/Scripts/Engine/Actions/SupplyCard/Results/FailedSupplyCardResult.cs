namespace RineaR.MadeHighlow
{
    public record FailedSupplyCardResult : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
