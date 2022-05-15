namespace RineaR.MadeHighlow.Actions.Valid
{
    public record FailedStepResult : StepResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
