namespace RineaR.MadeHighlow
{
    public record FailedStepResult : StepResult
    {
        public override World Simulate(in World world)
        {
            return world;
        }
    }
}