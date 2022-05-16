namespace RineaR.MadeHighlow.Actions.Valid
{
    public record ClimbMoveFailedResult : EntityStepResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
