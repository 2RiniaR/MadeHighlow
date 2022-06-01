using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteCard
{
    public record DeleteCardAction([NotNull] CardID TargetID);
}
