using JetBrains.Annotations;

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
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private Action Action { get; }

        private Player Parent { get; set; }
        private ValueList<Interrupt<CardReplacement>> Replacements { get; set; }
        private Event<ReactedResult<DropCard.SucceedResult>> DropCardEvent { get; set; }
        private Event<CreateCard.SucceedResult> CreateCardEvent { get; set; }
        private Process Process { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            Result result;

            result = FindParent();
            if (result != null) return result;

            result = AllocateDeckSpace();
            if (result != null) return result;

            result = CreateCard();
            if (result != null) return result;

            Process = new Process(DropCardEvent, CreateCardEvent);

            Context.Flows.CheckRejection(
                history: Simulating,
                contextProvider: (history, collected) => new RejectionContext(history, collected, Action, Process),
                onRejected: (rejection, rejectedID) => result = new RejectedResult(
                    Action,
                    Replacements,
                    Process,
                    rejection,
                    rejectedID
                )
            );
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private Result FindParent()
        {
            Parent = Context.Finder.FindPlayer(Simulating.World, Action.ParentID);
            if (Parent == null)
            {
                return new ParentNotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private Result AllocateDeckSpace()
        {
            if (ExistDeckSpace(Parent))
            {
                return null;
            }

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

                var succeedResult = result.BodyAs<DropCard.SucceedResult>();
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

        private void WrapProcess()
        {
            Process = new Process(DropCardEvent, CreateCardEvent);
        }

        [NotNull]
        private Result Succeed()
        {
            return new SucceedResult(Action, Replacements, Process);
        }
    }
}
