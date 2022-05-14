using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record ReactedResult(
        [NotNull] [ItemNotNull] ValueList<ReactedResult> Predictions,
        [NotNull] Result Body,
        [NotNull] [ItemNotNull] ValueList<ReactedResult> Reactions
    ) : ValidResult
    {
        public override World Simulate(World world)
        {
            return new Timeline().Then(Predictions).Then(Body).Then(Reactions).Simulate(world);
        }
    }

    public record ReactedResult<TResult>(
        [NotNull] [ItemNotNull] ValueList<ReactedResult> Predictions,
        [NotNull] TResult Body,
        [NotNull] [ItemNotNull] ValueList<ReactedResult> Reactions
    ) : ReactedResult(Predictions, Body, Reactions) where TResult : ValidResult
    {
        public new TResult Body { get; init; } = Body;

        [CanBeNull]
        public ReactedResult<TTarget> BodyAs<TTarget>() where TTarget : TResult
        {
            return Body is TTarget targetBody ? new ReactedResult<TTarget>(Predictions, targetBody, Reactions) : null;
        }
    }
}
