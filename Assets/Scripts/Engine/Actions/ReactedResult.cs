using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record ReactedResult(
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult<IValidResult>>> Predictions,
        [NotNull] Event Reacted,
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult<IValidResult>>> Reactions
    ) : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Timeline().Then(Predictions).Then(Reacted).Then(Reactions).Simulate(context, world);
        }
    }

    public record ReactedResult<TResult>(
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult<IValidResult>>> Predictions,
        [NotNull] Event<TResult> Reacted,
        [NotNull] [ItemNotNull] ValueList<Event<ReactedResult<IValidResult>>> Reactions
    ) : ReactedResult(Predictions, Reacted, Reactions) where TResult : IValidResult
    {
        public new Event<TResult> Reacted { get; init; } = Reacted;

        [CanBeNull]
        public ReactedResult<TTarget> BodyAs<TTarget>() where TTarget : TResult
        {
            return Reacted.Content is TTarget targetResult
                ? new ReactedResult<TTarget>(
                    Predictions,
                    new Event<TTarget>(Reacted.ID, Reacted.BeforeID, targetResult),
                    Reactions
                )
                : null;
        }
    }
}
