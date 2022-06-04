using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record ReactedResult(
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult<IValidResult>>> Predictions,
        [NotNull] Event Body,
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult<IValidResult>>> Reactions
    ) : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Timeline().Then(Predictions).Then(Body).Then(Reactions).Simulate(context, world);
        }
    }

    public record ReactedResult<TResult>(
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult<IValidResult>>> Predictions,
        [NotNull] Event<TResult> Body,
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult<IValidResult>>> Reactions
    ) : ReactedResult(Predictions, Body, Reactions) where TResult : IValidResult
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
