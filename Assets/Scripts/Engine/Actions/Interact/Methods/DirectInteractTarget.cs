using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Methods
{
    public record DirectInteractTarget
    {
        [NotNull] public EntityEnsuredID Pointer { get; init; } = new();

        [NotNull]
        [ItemNotNull]
        public ValueObjectList<IValidatable> Effects { get; init; } = ValueObjectList<IValidatable>.Empty;
    }
}