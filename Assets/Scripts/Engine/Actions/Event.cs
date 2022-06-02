using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record Event([NotNull] EventID ID, [NotNull] EventID BeforeID, [NotNull] IResult Result);

    public record Event<TResult>([NotNull] EventID ID, [NotNull] EventID BeforeID, [NotNull] TResult Result) : Event(
        ID,
        BeforeID,
        Result
    ) where TResult : IResult
    {
        public new TResult Result { get; init; } = Result;

        [CanBeNull]
        public Event<TTarget> ResultAs<TTarget>() where TTarget : TResult
        {
            return Result is TTarget targetResult ? new Event<TTarget>(ID, BeforeID, targetResult) : null;
        }
    }
}
