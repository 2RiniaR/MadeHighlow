using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.DropCard;

namespace RineaR.MadeHighlow.Actions.PlaceCard
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
            var parent = FindParent();

            if (parent == null) return Result;

            result = AllocateDeckSpace();
            if (result != null) return result;

            result = CreateCard();
            if (result != null) return result;

            Context.Flows.CheckRejection(
                history: Initial,
                contextProvider: (history, collected) => new RejectionContext(history, Result, collected),
                onRejected: rejection => { Result = Result with { Rejection = rejection }; }
            );

            return Succeed();
        }

        [CanBeNull]
        private Player FindParent()
        {
            return Context.Finder.FindPlayer(Initial.World, Action.ParentID);
        }

        private void AllocateDeckSpace(Player parent)
        {
            if (ExistDeckSpace(parent)) return;

            var effectors = Context.Finder.GetAllComponents<IReplacer>(Initial.World).Sort();
            Replacements = ValueList<Interrupt<CardReplacement>>.Empty;

            foreach (var effector in effectors)
            {
                var interrupts = effector.CardReplacements(Initial, Action, Replacements);
                if (interrupts == null) continue;
                Replacements = Replacements.AddRange(interrupts);
            }

            Replacements = Replacements.Sort();

            foreach (var interrupt in Replacements)
            {
                var result = Context.Actions.DropCard(Simulating, new DropCard.Action(interrupt.Effect.ReplacedID));

                var succeedResult = result.BodyAs<SucceedResult>();
                if (succeedResult == null)
                {
                    continue;
                }

                Simulating = Simulating.Appended(succeedResult, out var succeedEvent);

                var parent = Context.Finder.FindPlayer(Simulating.World, Action.ParentID);
                if (parent == null || !ExistDeckSpace(parent))
                {
                    continue;
                }

                DropCardEvent = succeedEvent;
                return null;
            }

            return new OverflowedResult(Action, Replacements);
        }

        private static bool ExistDeckSpace([NotNull] Player player)
        {
            return player.Cards.Count < player.DeckSize.Value;
        }

        [CanBeNull]
        private Result CreateCard()
        {
            var result = Context.Actions.CreateCard(
                Simulating,
                new CreateCard.Action(Action.ParentID, Action.InitialProps)
            );
            if (result is not CreateCard.SucceedResult succeedResult)
            {
                return new CreateCardFailedResult(Action, DropCardEvent, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            CreateCardEvent = succeedEvent;

            return null;
        }

        [NotNull]
        private Result Succeed()
        {
            return new SucceedResult(Action, Replacements, Process);
        }
    }
}
