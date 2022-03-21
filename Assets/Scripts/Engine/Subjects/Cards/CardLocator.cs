using RineaR.MadeHighlow.Engine.Subjects.Players;

namespace RineaR.MadeHighlow.Engine.Subjects.Cards
{
    public record CardLocator : PlayerLocator
    {
        public ID<Card> CardID { get; init; } = ID<Card>.None;
    }
}