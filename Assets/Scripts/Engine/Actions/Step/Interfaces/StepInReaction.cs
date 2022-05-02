namespace RineaR.MadeHighlow.Actions
{
    public record StepInReaction
    {
        public EntityLocator Reactor { get; init; } = new();
        public Result Result { get; init; } = new EmptyResult();
    }
}