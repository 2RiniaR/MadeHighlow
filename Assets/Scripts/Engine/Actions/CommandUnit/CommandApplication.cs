using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CommandUnit
{
    public record CommandApplication
    {
        [NotNull] public ObjectLocator Target { get; init; } = new();
        [NotNull] public CommandOperation Operation { get; init; } = new();
    }
}