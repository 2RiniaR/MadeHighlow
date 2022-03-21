using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects.Cards;
using RineaR.MadeHighlow.Engine.Subjects.Cards.Commands;

namespace RineaR.MadeHighlow.Engine.Subjects.Objects.Units
{
    public record CommandOperation
    {
        public ID<Card> CardID { get; init; } = ID<Card>.None;
        [CanBeNull] public CommandOption Option { get; init; } = null;
    }
}