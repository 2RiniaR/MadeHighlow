using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public record Route([NotNull] [ItemNotNull] ValueList<Step> Steps);
}
