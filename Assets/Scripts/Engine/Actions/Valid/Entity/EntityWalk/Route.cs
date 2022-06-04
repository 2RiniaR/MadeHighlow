using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityWalk
{
    public record Route([NotNull] [ItemNotNull] ValueList<Step> Steps);
}
