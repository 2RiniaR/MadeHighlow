using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PayCard
{
    public record PayCardAction([NotNull] CardID TargetID) : IValidAction;
}
