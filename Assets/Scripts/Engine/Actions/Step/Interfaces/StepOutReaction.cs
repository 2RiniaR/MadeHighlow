namespace RineaR.MadeHighlow
{
    public record StepOutReaction
    {
        public EntityID Reactor { get; init; } = new();
        public Result Result { get; init; } = Result.Empty;
    }
}