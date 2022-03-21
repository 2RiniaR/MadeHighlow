using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects.Objects;
using RineaR.MadeHighlow.Engine.Subjects.Objects.Units;

namespace RineaR.MadeHighlow.Engine.Actions.Command
{
    public record CommandApplication
    {
        [NotNull] public ObjectLocator Target { get; init; } = new();
        [NotNull] public CommandOperation Operation { get; init; } = new();
    }
}