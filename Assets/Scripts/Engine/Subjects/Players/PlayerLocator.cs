namespace RineaR.MadeHighlow
{
    public record PlayerLocator
    {
        public ID<Player> PlayerID { get; init; } = ID<Player>.None;
    }
}