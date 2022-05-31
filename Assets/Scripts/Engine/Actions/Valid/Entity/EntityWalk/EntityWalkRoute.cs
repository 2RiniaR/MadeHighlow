using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityWalk
{
    public record EntityWalkRoute([NotNull] [ItemNotNull] ValueList<EntityWalkStep> Steps);
}
