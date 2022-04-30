using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record CommandOperation
    {
        public ID<Card> CardID { get; init; } = ID<Card>.None;
        [CanBeNull] public CommandOption Option { get; init; } = null;
    }
}