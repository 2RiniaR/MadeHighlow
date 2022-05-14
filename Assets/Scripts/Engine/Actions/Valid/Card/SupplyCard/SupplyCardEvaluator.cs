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
        public SupplyCardEvaluator(
            [NotNull] IHistory history,
            [NotNull] PlayerID targetID,
            [NotNull] Card initialStatus
        )
        {
            History = history;
            TargetID = targetID;
            InitialStatus = initialStatus;
        }

        [NotNull] private IHistory History { get; set; }
        [NotNull] private PlayerID TargetID { get; }
        [NotNull] private Card InitialStatus { get; }

        [CanBeNull] private Fragment.RegisterCard.SucceedResult RegisterCardResult { get; set; }
        [CanBeNull] private ValueList<ReactedResult<AddComponent.SucceedResult>> AddComponentResults { get; set; }
        [CanBeNull] private Fragment.PutCard.SucceedResult PutCardResult { get; set; }
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

            result = GetGenerating();
            if (result != null) return result;

            // 「カードを供給できるか」の判定に、交換で破棄されたカードのコンポーネントは干渉ができない
            result = CollectInterrupts();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private SupplyCardResult RegisterCard()
        {
            Contract.Ensures(
                (Contract.Result<SupplyCardResult>() != null) ^ (RegisterCardResult != null && Generating != null)
            );

            var result = new RegisterCardAction(TargetID, InitialStatus).Evaluate(History);
            if (result is not Fragment.RegisterCard.SucceedResult succeedResult)
            {
                return new RegisterFailedResult(TargetID, InitialStatus, result);
            }

            History = History.Appended(succeedResult);
            RegisterCardResult = succeedResult;
            Generating = succeedResult.Registered;
            return null;
        }

        [CanBeNull]
        private SupplyCardResult AddInitialComponents()
        {
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Requires<InvalidOperationException>(RegisterCardResult != null);
            Contract.Ensures(AddComponentResults != null);

            AddComponentResults = ValueList<ReactedResult<AddComponent.SucceedResult>>.Empty;
            foreach (var component in InitialStatus.Components)
            {
                var result = new AddComponentAction(Generating.CardID, component).Evaluate(History);
                var succeedResult = result.BodyAs<AddComponent.SucceedResult>();
                if (succeedResult == null)
                {
                    return new AddComponentFailedResult(
                        TargetID,
                        InitialStatus,
                        RegisterCardResult,
                        AddComponentResults,
                        result
                    );
                }

                History = History.Appended(succeedResult);
                AddComponentResults = AddComponentResults.Add(succeedResult);
            }

            return null;
        }

        [CanBeNull]
        private SupplyCardResult PutCard()
        {
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Requires<InvalidOperationException>(RegisterCardResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Ensures((Contract.Result<SupplyCardResult>() != null) ^ (PutCardResult != null));

            var result = new PutCardAction(Generating.CardID).Evaluate(History);
            if (result is not Fragment.PutCard.SucceedResult succeedResult)
            {
                return new PutCardFailedResult(
                    TargetID,
                    InitialStatus,
                    RegisterCardResult,
                    AddComponentResults,
                    result
                );
            }

            History = History.Appended(succeedResult);
            PutCardResult = succeedResult;

            return null;
        }

        [CanBeNull]
        private SupplyCardResult GetGenerating()
        {
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Requires<InvalidOperationException>(RegisterCardResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Requires<InvalidOperationException>(PutCardResult != null);
            Contract.Ensures((Contract.Result<SupplyCardResult>() != null) ^ (Generating != null));

            Generating = Generating.CardID.GetFrom(History.World);
            if (Generating == null)
            {
                return new DestroyedResult(
                    TargetID,
                    InitialStatus,
                    RegisterCardResult,
                    AddComponentResults,
                    PutCardResult
                );
            }

            return null;
        }

        [CanBeNull]
        private SupplyCardResult CollectInterrupts()
        {
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Requires<InvalidOperationException>(RegisterCardResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Requires<InvalidOperationException>(PutCardResult != null);
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Ensures(Interrupts != null);

            var effectors = Component.GetAllOfTypeFrom<ISupplyCardEffector>(History.World);
            Interrupts = effectors.SelectMany(effector => effector.EffectsOnSupplyCard(History, Generating)).Sort();
            foreach (var interrupt in Interrupts)
            {
                if (interrupt.Effect is RejectEffect)
                {
                    return new RejectedResult(
                        TargetID,
                        InitialStatus,
                        RegisterCardResult,
                        AddComponentResults,
                        PutCardResult,
                        Interrupts,
                        interrupt.ComponentID
                    );
                }
            }

            return null;
        }

        [NotNull]
        private SupplyCardResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Requires<InvalidOperationException>(RegisterCardResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Requires<InvalidOperationException>(PutCardResult != null);
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Requires<InvalidOperationException>(Interrupts != null);

            return new SucceedResult(
                TargetID,
                InitialStatus,
                RegisterCardResult,
                AddComponentResults,
                PutCardResult,
                Interrupts,
                Generating
            );
        }
    }
}
