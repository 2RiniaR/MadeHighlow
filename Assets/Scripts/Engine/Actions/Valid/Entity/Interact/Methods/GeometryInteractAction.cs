using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record GeometryInteractAction
        ([NotNull] [ItemNotNull] ValueList<GeometryInteractTarget> Targets) : InteractAction;
}
