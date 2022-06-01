using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DropCard
{
    public record DropCardAction([NotNull] CardID TargetID) : IValidAction;
}
