namespace RineaR.MadeHighlow
{
    public record StepOutReaction
    {
        public EntityEnsuredID Reactor { get; init; } = new();
        public ISimulatable Result { get; init; } = ISimulatable.Empty;
    }
}