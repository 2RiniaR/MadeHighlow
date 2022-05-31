using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public record EntityFlyRoute([NotNull] [ItemNotNull] ValueList<EntityFlyStep> Steps);
}
