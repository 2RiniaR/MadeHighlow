using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record UnitOperation
    {
        [NotNull] public CardEnsuredID CardID { get; init; } = new();
        [NotNull] public CommandOption Option { get; init; } = CommandOption.Empty;
    }
}