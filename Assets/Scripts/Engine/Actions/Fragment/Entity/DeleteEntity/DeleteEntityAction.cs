using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteEntity
{
    public record DeleteEntityAction([NotNull] EntityID TargetID);
}
