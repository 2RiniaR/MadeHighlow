using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.UnregisterEntity
{
    public record UnregisterEntityAction([NotNull] EntityID TargetID);
}
