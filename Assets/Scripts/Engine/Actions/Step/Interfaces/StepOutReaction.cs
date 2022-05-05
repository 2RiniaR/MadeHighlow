namespace RineaR.MadeHighlow
{
    public record StepOutReaction
    {
        public EntityEnsuredID Reactor { get; init; } = new();
        public Result Result { get; init; } = Result.Empty;
    }
}