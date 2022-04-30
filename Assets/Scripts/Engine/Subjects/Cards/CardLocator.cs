namespace RineaR.MadeHighlow
{
    public record CardLocator : PlayerLocator
    {
        public ID<Card> CardID { get; init; } = ID<Card>.None;
    }
}