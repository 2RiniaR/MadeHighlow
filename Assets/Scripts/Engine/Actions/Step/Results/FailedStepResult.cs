namespace RineaR.MadeHighlow
{
    public record FailedStepResult : StepResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
