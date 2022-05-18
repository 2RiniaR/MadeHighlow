using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.PutCard;
using RineaR.MadeHighlow.Actions.Fragment.RegisterCard;
using RineaR.MadeHighlow.Actions.Valid.AddComponent;

namespace RineaR.MadeHighlow.Actions.Valid.SupplyCard
{
    public class SupplyCardEvaluator
    {
        public SupplyCardEvaluator([NotNull] IHistory initial, SupplyCardAction action)
        {
            Initial = initial;
            Action = action;
            Simulating = Initial;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private IHistory Simulating { get; set; }
        [NotNull] private SupplyCardAction Action { get; }

        [CanBeNull] private Event<Fragment.RegisterCard.SucceedResult> RegisterCardEvent { get; set; }
        [CanBeNull] private ValueList<Event<ReactedResult<AddComponent.SucceedResult>>> AddComponentEvents { get; set; }
        [CanBeNull] private Event<Fragment.PutCard.SucceedResult> PutCardEvent { get; set; }

        [CanBeNull] private Process Process { get; set; }
        [CanBeNull] private ValueList<Interrupt<SupplyCardEffect>> Interrupts { get; set; }
        [CanBeNull] private Card Generating { get; set; }

        [NotNull]
        public SupplyCardResult Evaluate()
        {
            SupplyCardResult result;

            result = RegisterCard();
            if (result != null) return result;

            result = AddInitialComponents();
            if (result != null) return result;

            result = PutCard();
            if (result != null) return result;

            WrapProcess();

            result = FindGenerating();
            if (result != null) return result;

            CollectInterrupts();

            result = CheckRejection();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private SupplyCardResult RegisterCard()
        {
            Contract.Ensures(
                (Contract.Result<SupplyCardResult>() != null) ^ (RegisterCardEvent != null && Generating != null)
            );

            var result = new RegisterCardAction(Action.TargetID, Action.InitialStatus).Evaluate(Simulating);
            if (result is not Fragment.RegisterCard.SucceedResult succeedResult)
            {
                return new RegisterFailedResult(Action, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            RegisterCardEvent = succeedEvent;
            Generating = succeedResult.Registered;

            return null;
        }

        [CanBeNull]
        private SupplyCardResult AddInitialComponents()
        {
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Requires<InvalidOperationException>(RegisterCardEvent != null);
            Contract.Ensures(AddComponentEvents != null);

            AddComponentEvents = ValueList<Event<ReactedResult<AddComponent.SucceedResult>>>.Empty;

            foreach (var component in Action.InitialStatus.Components)
            {
                var result = new AddComponentAction(Generating.CardID, component).Evaluate(Simulating);

                var succeedResult = result.BodyAs<AddComponent.SucceedResult>();
                if (succeedResult == null)
                {
                    return new AddComponentFailedResult(Action, RegisterCardEvent, AddComponentEvents, result);
                }

                Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
                AddComponentEvents = AddComponentEvents.Add(succeedEvent);
            }

            return null;
        }

        [CanBeNull]
        private SupplyCardResult PutCard()
        {
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Requires<InvalidOperationException>(RegisterCardEvent != null);
            Contract.Requires<InvalidOperationException>(AddComponentEvents != null);
            Contract.Ensures((Contract.Result<SupplyCardResult>() != null) ^ (PutCardEvent != null));

            var result = new PutCardAction(Generating.CardID).Evaluate(Simulating);
            if (result is not Fragment.PutCard.SucceedResult succeedResult)
            {
                return new PutCardFailedResult(Action, RegisterCardEvent, AddComponentEvents, result);
            }

            Simulating = Simulating.Appended(succeedResult, out var succeedEvent);
            PutCardEvent = succeedEvent;

            return null;
        }

        private void WrapProcess()
        {
            Contract.Requires<InvalidOperationException>(RegisterCardEvent != null);
            Contract.Requires<InvalidOperationException>(AddComponentEvents != null);
            Contract.Requires<InvalidOperationException>(PutCardEvent != null);
            Contract.Ensures(Process != null);

            Process = new Process(RegisterCardEvent, AddComponentEvents, PutCardEvent);
        }

        [CanBeNull]
        private SupplyCardResult FindGenerating()
        {
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures((Contract.Result<SupplyCardResult>() != null) ^ (Generating != null));

            Generating = Generating.CardID.GetFrom(Simulating.World);
            if (Generating == null)
            {
                return new DestroyedResult(Action, Process);
            }

            return null;
        }

        private void CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<ISupplyCardEffector>(Initial.World).Sort();

            Interrupts = ValueList<Interrupt<SupplyCardEffect>>.Empty;
            foreach (var effector in effectors)
            {
                var interrupts = effector.EffectsOnSupplyCard(Simulating, Process);
                Interrupts = Interrupts.AddRange(interrupts);
            }
        }

        [CanBeNull]
        private SupplyCardResult CheckRejection()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(Action, Process, Interrupts, interrupt.ComponentID);
                }
            }

            return null;
        }

        [NotNull]
        private SupplyCardResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Process != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            return new SucceedResult(Action, Process, Interrupts);
        }
    }
}
