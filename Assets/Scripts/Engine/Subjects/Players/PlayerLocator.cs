namespace RineaR.MadeHighlow.Engine.Subjects.Players
{
    public record PlayerLocator
    {
        public ID<Player> PlayerID { get; init; } = ID<Player>.None;
    }
}