using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record ReactedResult(
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult>> Predictions,
        [NotNull] Event Body,
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult>> Reactions
    ) : ValidResult
    {
        public override World Simulate(World world)
        {
            return new Timeline().Then(Predictions).Then(Body).Then(Reactions).Simulate(world);
        }
    }

    public record ReactedResult<TResult>(
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult>> Predictions,
        [NotNull] Event<TResult> Body,
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult>> Reactions
    ) : ReactedResult(Predictions, Body, Reactions) where TResult : ValidResult
    {
        public new Event<TResult> Body { get; init; } = Body;

        [CanBeNull]
        public ReactedResult<TTarget> BodyAs<TTarget>() where TTarget : TResult
        {
            return Body.Result is TTarget targetResult
                ? new ReactedResult<TTarget>(
                    Predictions,
                    new Event<TTarget>(Body.ID, Body.BeforeID, targetResult),
                    Reactions
                )
                : null;
        }
    }
}
