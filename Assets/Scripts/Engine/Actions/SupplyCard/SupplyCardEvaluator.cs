using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AddComponent;
using RineaR.MadeHighlow.Actions.SupplyCard.PutCard;
using RineaR.MadeHighlow.Actions.SupplyCard.RegisterCard;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public class SupplyCardEvaluator
    {
        public SupplyCardEvaluator(
            [NotNull] IActionContext context,
            [NotNull] PlayerID targetID,
            [NotNull] Card initialStatus
        )
        {
            Context = context;
            TargetID = targetID;
            InitialStatus = initialStatus;
        }

        [NotNull] private IActionContext Context { get; set; }
        [NotNull] private PlayerID TargetID { get; }
        [NotNull] private Card InitialStatus { get; }

        [CanBeNull] private RegisterCard.SucceedResult RegisterCardResult { get; set; }
        [CanBeNull] private ValueList<AddComponent.SucceedResult> AddComponentResults { get; set; }
        [CanBeNull] private PutCard.SucceedResult PutCardResult { get; set; }
        [CanBeNull] private ValueList<Interrupt<SupplyCardEffect>> Interrupts { get; set; }

        [CanBeNull] private Player Target { get; set; }
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

            var result = new RegisterCardAction(TargetID, InitialStatus).Evaluate(Context);
            if (result is not RegisterCard.SucceedResult succeedResult)
            {
                return new RegisterFailedResult(TargetID, InitialStatus, result);
            }

            Context = Context.Appended(succeedResult);
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

            AddComponentResults = ValueList<AddComponent.SucceedResult>.Empty;
            foreach (var component in InitialStatus.Components)
            {
                var result = new AddComponentAction(Generating.CardID, component).Evaluate(Context);
                if (result is not AddComponent.SucceedResult succeedResult)
                {
                    return new AddComponentFailedResult(
                        TargetID,
                        InitialStatus,
                        RegisterCardResult,
                        AddComponentResults,
                        result
                    );
                }

                Context = Context.Appended(succeedResult);
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

            var result = new PutCardAction(Generating.CardID).Evaluate(Context);
            if (result is not PutCard.SucceedResult succeedResult)
            {
                return new PutCardFailedResult(
                    TargetID,
                    InitialStatus,
                    RegisterCardResult,
                    AddComponentResults,
                    result
                );
            }

            Context = Context.Appended(succeedResult);
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

            Generating = Generating.CardID.GetFrom(Context.World);
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

            var effectors = Component.GetAllOfTypeFrom<ISupplyCardEffector>(Context.World);
            Interrupts = effectors.SelectMany(effector => effector.EffectsOnSupplyCard(Context, Generating)).Sort();
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
