using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public sealed record Timeline()
    {
        private Timeline([NotNull] [ItemNotNull] ValueList<Result> results) : this()
        {
            Results = results;
        }

        [NotNull] [ItemNotNull] public ValueList<Result> Results { get; }

        [NotNull]
        public Timeline Then([NotNull] Result result)
        {
            return new Timeline(Results.Add(result));
        }

        [NotNull]
        public Timeline Then([NotNull] [ItemNotNull] in ValueList<Result> results)
        {
            return new Timeline(Results.AddRange(results));
        }

        [NotNull]
        public Timeline Then<TResult>([NotNull] [ItemNotNull] ValueList<TResult> results) where TResult : Result
        {
            return new Timeline(Results.AddRange(results));
        }

        [NotNull]
        public World Simulate([NotNull] World world)
        {
            return Results.Aggregate(world, (current, result) => result.Simulate(current));
        }
    }
}
