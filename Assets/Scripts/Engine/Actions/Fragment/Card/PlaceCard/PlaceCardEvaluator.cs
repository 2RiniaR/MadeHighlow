using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.CreateCard;
using RineaR.MadeHighlow.Actions.DropCard;

namespace RineaR.MadeHighlow.Actions.PlaceCard
{
    public class PlaceCardEvaluator
    {
        public PlaceCardEvaluator([NotNull] ActionContext context, [NotNull] IHistory initial, PlaceCardAction action)
        {
            Initial = initial;
            Context = context;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private ActionContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private PlaceCardAction Action { get; }

        [CanBeNull] private Player Parent { get; set; }
        [CanBeNull] private ValueList<Interrupt<CardReplacement>> ReplacementInterrupts { get; set; }
        [CanBeNull] private Event<ReactedResult<DropCard.SucceedResult>> DropCardEvent { get; set; }
        [CanBeNull] private Event<CreateCard.SucceedResult> CreateCardEvent { get; set; }
        [CanBeNull] private PlaceCardProcess Process { get; set; }
        [CanBeNull] private ValueList<Interrupt<PlaceCardRejection>> RejectionInterrupts { get; set; }

        [NotNull]
        public PlaceCardResult Evaluate()
        {
            PlaceCardResult result;

            result = FindParent();
            if (result != null) return result;

            result = AllocateDeckSpace();
            if (result != null) return result;

            result = CreateCard();
            if (result != null) return result;

            WrapProcess();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private PlaceCardResult FindParent()
        {
            Parent = Context.Finder.FindPlayer(Simulating.World, Action.ParentID);
            if (Parent == null)
            {
                return new ParentNotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private PlaceCardResult AllocateDeckSpace()
        {
            if (ExistDeckSpace(Parent))
            {
                return null;
            }

            var effectors = Component.GetAllOfTypeFrom<IPlaceCardReplacer>(Initial.World).Sort();
            ReplacementInterrupts = ValueList<Interrupt<CardReplacement>>.Empty;

            foreach (var effector in effectors)
            {
                var interrupts = effector.CardReplacements(Initial, Action, ReplacementInterrupts);
                if (interrupts == null) continue;
                ReplacementInterrupts = ReplacementInterrupts.AddRange(interrupts);
            }

            ReplacementInterrupts = ReplacementInterrupts.Sort();

            foreach (var interrupt in ReplacementInterrupts)
            {
                var result = Context.Actions.DropCard(Simulating, new DropCardAction(interrupt.Effect.ReplacedID));

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

            return new OverflowedResult(Action, ReplacementInterrupts);
        }

        private static bool ExistDeckSpace([NotNull] Player player)
        {
            return player.Cards.Count < player.DeckSize.Value;
        }

        [CanBeNull]
        private PlaceCardResult CreateCard()
        {
            var result = Context.Actions.CreateCard(
                Simulating,
                new CreateCardAction(Action.ParentID, Action.InitialProps)
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
            Process = new PlaceCardProcess(DropCardEvent, CreateCardEvent);
        }

        [CanBeNull]
        private PlaceCardResult CheckRejection()
        {
            var effectors = Component.GetAllOfTypeFrom<IPlaceCardRejector>(Initial.World).Sort();

            RejectionInterrupts = ValueList<Interrupt<PlaceCardRejection>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupt = effector.PlaceCardRejection(Simulating, Action, Process, RejectionInterrupts);
                if (interrupt == null) continue;
                RejectionInterrupts = RejectionInterrupts.Add(interrupt);
            }

            if (!RejectionInterrupts.IsEmpty)
            {
                return new RejectedResult(
                    Action,
                    ReplacementInterrupts,
                    Process,
                    RejectionInterrupts,
                    RejectionInterrupts[0].ComponentID
                );
            }

            return null;
        }

        [NotNull]
        private PlaceCardResult Succeed()
        {
            return new SucceedResult(Action, ReplacementInterrupts, Process, RejectionInterrupts);
        }
    }
}
