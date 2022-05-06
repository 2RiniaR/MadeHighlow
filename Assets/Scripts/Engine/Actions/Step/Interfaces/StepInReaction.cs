namespace RineaR.MadeHighlow
{
    public record StepInReaction
    {
        public EntityID Reactor { get; init; } = new();
        public Result Result { get; init; } = Result.Empty;
    }
}