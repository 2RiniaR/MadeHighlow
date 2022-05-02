using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Directors
{
    public record CommandApplication
    {
        [NotNull] public EntityLocator Target { get; init; } = new();
        [NotNull] public UnitOperation Operation { get; init; } = new();
    }
}