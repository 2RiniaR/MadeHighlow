namespace RineaR.MadeHighlow.Actions.Valid
{
    public record RejectedResult : EntityStepResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
