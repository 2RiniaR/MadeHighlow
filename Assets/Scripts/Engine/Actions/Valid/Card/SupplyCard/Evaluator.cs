using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, Action action)
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
            Result = new Result(Action);
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private Action Action { get; }
        [NotNull] private Result Result { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            PlaceCard();

            if (Result.PlaceCard.Content.CreateCard.Content.Created == null) return Result;

            Context.Flows.CheckRejection(
                history: Initial,
                contextProvider: (history, collected) => new RejectionContext(history, Result, collected),
                onRejected: rejection => { Result = Result with { Rejection = rejection }; }
            );

            if (Result.Rejection != null) return Result;

            Confirm();

            return Result;
        }

        private void PlaceCard()
        {
            var action = new PlaceCard.Action(Action.TargetID, Action.InitialStatus);
            var result = Context.Actions.PlaceCard(Simulating, action);
            Simulating = Simulating.Appended(result, out var @event);
            Result = Result with { PlaceCard = @event };
        }

        private void Confirm()
        {
            Result = Result with { Supplied = Result.PlaceCard.Content.CreateCard.Content.Created };
        }
    }
}
