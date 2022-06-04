using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateCard
{
    public record RegisterCardFailedResult([NotNull] Action Action, [NotNull] RegisterCard.Result Failed) : Result;
}
