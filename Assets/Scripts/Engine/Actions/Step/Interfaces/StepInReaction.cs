namespace RineaR.MadeHighlow
{
    public record StepInReaction
    {
        public EntityEnsuredID Reactor { get; init; } = new();
        public Result Result { get; init; } = Result.Empty;
    }
}