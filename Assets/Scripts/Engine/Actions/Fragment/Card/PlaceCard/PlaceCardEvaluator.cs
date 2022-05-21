using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.CreateCard;
using RineaR.MadeHighlow.Actions.Valid.DropCard;

namespace RineaR.MadeHighlow.Actions.Fragment.PlaceCard
{
    public class PlaceCardEvaluator
    {
        public PlaceCardEvaluator([NotNull] IHistory initial, PlaceCardAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private PlaceCardAction Action { get; }

        [CanBeNull] private Player Parent { get; set; }
        [CanBeNull] private ValueList<Interrupt<CardReplacement>> ReplacementInterrupts { get; set; }
        [CanBeNull] private Event<ReactedResult<Valid.DropCard.SucceedResult>> DropCardEvent { get; set; }
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
            Contract.Ensures((Contract.Result<PlaceCardResult>() != null) ^ (Parent != null));

            Parent = Action.ParentID.GetFrom(Simulating.World);
            if (Parent == null)
            {
                return new ParentNotFoundResult(Action);
            }

            return null;
        }

        [CanBeNull]
        private PlaceCardResult AllocateDeckSpace()
        {
            Contract.Requires<InvalidOperationException>(Parent != null);

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
                var result = new DropCardAction(interrupt.Effect.ReplacedID).Evaluate(Simulating);

                var succeedResult = result.BodyAs<Valid.DropCard.SucceedResult>();
                if (succeedResult == null)
                {
                    continue;
                }

                Simulating = Simulating.Appended(succeedResult, out var succeedEvent);

                var parent = Action.ParentID.GetFrom(Simulating.World);
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
            var result = new CreateCardAction(Action.ParentID, Action.InitialProps).Evaluate(Simulating);
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
            Contract.Requires<InvalidOperationException>(CreateCardEvent != null);
            Contract.Ensures(Process != null);

            Process = new PlaceCardProcess(DropCardEvent, CreateCardEvent);
        }

        [CanBeNull]
        private PlaceCardResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures(RejectionInterrupts != null);

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
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(RejectionInterrupts != null);

            return new SucceedResult(Action, ReplacementInterrupts, Process, RejectionInterrupts);
        }
    }
}
