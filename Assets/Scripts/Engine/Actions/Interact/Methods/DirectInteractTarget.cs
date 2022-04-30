using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Interact.Methods
{
    public record DirectInteractTarget
    {
        [NotNull] public ObjectLocator Pointer { get; init; } = new();

        [NotNull]
        [ItemNotNull]
        public ValueObjectList<InteractionEffect> Effects { get; init; } = ValueObjectList<InteractionEffect>.Empty;
    }
}