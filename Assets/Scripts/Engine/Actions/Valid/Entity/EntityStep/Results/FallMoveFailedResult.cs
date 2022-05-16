namespace RineaR.MadeHighlow.Actions.Valid
{
    public record FallMoveFailedResult : EntityStepResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
