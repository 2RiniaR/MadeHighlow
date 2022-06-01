using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public record DestroyEntityAction([NotNull] EntityID TargetID) : IValidAction;
}
