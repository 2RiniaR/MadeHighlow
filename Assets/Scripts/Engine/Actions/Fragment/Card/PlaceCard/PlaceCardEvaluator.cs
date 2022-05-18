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
        [CanBeNull] private ValueList<Interrupt<PlaceCardReplaceEffect>> ReplaceInterrupts { get; set; }
        [CanBeNull] private Event<ReactedResult<Valid.DropCard.SucceedResult>> DropCardEvent { get; set; }
        [CanBeNull] private Event<CreateCard.SucceedResult> CreateCardEvent { get; set; }
        [CanBeNull] private Process Process { get; set; }
        [CanBeNull] private ValueList<Interrupt<PlaceCardEffect>> Interrupts { get; set; }

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
            CollectInterrupts();

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

            var effectors = Component.GetAllOfTypeFrom<IPlaceCardReplaceEffector>(Initial.World).Sort();
            ReplaceInterrupts = ValueList<Interrupt<PlaceCardReplaceEffect>>.Empty;

            foreach (var effector in effectors)
            {
                var interrupts = effector.ReplaceEffectsOnPutCard(Initial, Action);
                ReplaceInterrupts = ReplaceInterrupts.AddRange(interrupts);
            }

            ReplaceInterrupts = ReplaceInterrupts.Sort();

            foreach (var interrupt in ReplaceInterrupts)
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

            return new OverflowedResult(Action, ReplaceInterrupts);
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

            Process = new Process(DropCardEvent, CreateCardEvent);
        }

        private void CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<IPlaceCardEffector>(Initial.World).Sort();

            Interrupts = ValueList<Interrupt<PlaceCardEffect>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.EffectsOnPlaceCard(Simulating, Action, Process);
                Interrupts = Interrupts.AddRange(interrupts);
            }
        }

        [CanBeNull]
        private PlaceCardResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(Action, ReplaceInterrupts, Process, Interrupts, interrupt.ComponentID);
                }
            }

            return null;
        }

        [NotNull]
        private PlaceCardResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            return new SucceedResult(Action, ReplaceInterrupts, Process, Interrupts);
        }
    }
}
