namespace RineaR.MadeHighlow.Actions
{
    public record StepOutReaction
    {
        public EntityLocator Reactor { get; init; } = new();
        public Result Result { get; init; } = new EmptyResult();
    }
}