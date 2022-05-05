namespace RineaR.MadeHighlow
{
    public record StepInReaction
    {
        public EntityEnsuredID Reactor { get; init; } = new();
        public ISimulatable Result { get; init; } = ISimulatable.Empty;
    }
}