using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record UnitOperation
    {
        public ID<Card> CardID { get; init; } = ID<Card>.None;
        [NotNull] public CommandOption Option { get; init; } = new EmptyCommandOption();
    }
}