using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DirectInteractTarget
    {
        [NotNull] public EntityID Pointer { get; init; } = new();

        [NotNull] [ItemNotNull] public ValueObjectList<Action> Effects { get; init; } = ValueObjectList<Action>.Empty;
    }
}