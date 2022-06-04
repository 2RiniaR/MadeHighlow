using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record Event([NotNull] EventID ID, [NotNull] EventID BeforeID, [NotNull] IResult Content);

    public record Event<TResult>([NotNull] EventID ID, [NotNull] EventID BeforeID, [NotNull] TResult Content) : Event(
        ID,
        BeforeID,
        Content
    ) where TResult : IResult
    {
        public new TResult Content { get; init; } = Content;

        [CanBeNull]
        public Event<TTarget> ResultAs<TTarget>() where TTarget : TResult
        {
            return Content is TTarget targetResult ? new Event<TTarget>(ID, BeforeID, targetResult) : null;
        }
    }
}
