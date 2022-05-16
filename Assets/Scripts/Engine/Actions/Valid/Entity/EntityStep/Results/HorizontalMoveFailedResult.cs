namespace RineaR.MadeHighlow.Actions.Valid
{
    public record HorizontalMoveFailedResult : EntityStepResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
