using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AddComponent;
using RineaR.MadeHighlow.Actions.JoinPlayer.RegisterPlayer;
using RineaR.MadeHighlow.Actions.SupplyCard;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public class JoinPlayerEvaluator
    {
        public JoinPlayerEvaluator([NotNull] IActionContext context, [NotNull] Player initialStatus)
        {
            Context = context;
            InitialStatus = initialStatus;
        }

        [NotNull] private IActionContext Context { get; set; }
        [NotNull] private Player InitialStatus { get; }

        [CanBeNull] private RegisterPlayer.SucceedResult RegisterPlayerResult { get; set; }
        [CanBeNull] private ValueList<AddComponent.SucceedResult> AddComponentResults { get; set; }
        [CanBeNull] private ValueList<SupplyCard.SucceedResult> SupplyCardResults { get; set; }

        [CanBeNull] private Player Generating { get; set; }

        [NotNull]
        public JoinPlayerResult Evaluate()
        {
            JoinPlayerResult result;

            result = RegisterPlayer();
            if (result != null) return result;

            result = AddComponents();
            if (result != null) return result;

            result = SupplyCards();
            if (result != null) return result;

            result = GetPlayer();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private JoinPlayerResult RegisterPlayer()
        {
            Contract.Ensures(
                (Contract.Result<JoinPlayerResult>() != null) ^ (RegisterPlayerResult != null && Generating != null)
            );

            var result = new RegisterPlayerAction(InitialStatus).Evaluate(Context);
            if (result is not RegisterPlayer.SucceedResult succeedResult)
            {
                return new RegisterFailedResult(InitialStatus, result);
            }

            Context = Context.Appended(succeedResult);
            RegisterPlayerResult = succeedResult;
            Generating = succeedResult.Registered;

            return null;
        }

        [CanBeNull]
        private JoinPlayerResult AddComponents()
        {
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Requires<InvalidOperationException>(RegisterPlayerResult != null);
            Contract.Ensures(AddComponentResults != null);

            AddComponentResults = ValueList<AddComponent.SucceedResult>.Empty;
            foreach (var component in InitialStatus.Components)
            {
                var result = new AddComponentAction(Generating.PlayerID, component).Evaluate(Context);
                if (result is not AddComponent.SucceedResult succeedResult)
                {
                    return new AddComponentFailedResult(
                        InitialStatus,
                        RegisterPlayerResult,
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
        private JoinPlayerResult SupplyCards()
        {
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Requires<InvalidOperationException>(RegisterPlayerResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Ensures(SupplyCardResults != null);

            SupplyCardResults = ValueList<SupplyCard.SucceedResult>.Empty;
            foreach (var card in InitialStatus.Cards)
            {
                var result = new SupplyCardAction(Generating.PlayerID, card).Evaluate(Context);
                if (result is not SupplyCard.SucceedResult succeedResult)
                {
                    return new SupplyCardFailedResult(
                        InitialStatus,
                        RegisterPlayerResult,
                        AddComponentResults,
                        SupplyCardResults,
                        result
                    );
                }

                Context = Context.Appended(succeedResult);
                SupplyCardResults = SupplyCardResults.Add(succeedResult);
            }

            return null;
        }

        [CanBeNull]
        private JoinPlayerResult GetPlayer()
        {
            Contract.Requires<InvalidOperationException>(Generating != null);
            Contract.Requires<InvalidOperationException>(RegisterPlayerResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Requires<InvalidOperationException>(SupplyCardResults != null);
            Contract.Ensures((Contract.Result<JoinPlayerResult>() != null) ^ (Generating != null));

            // `RegisterPlayer` アクション実行後に、副作用で対象のエンティティが削除される可能性がある
            Generating = Generating.PlayerID.GetFrom(Context.World);
            if (Generating == null)
            {
                return new DestroyedResult(InitialStatus, RegisterPlayerResult, AddComponentResults, SupplyCardResults);
            }

            return null;
        }

        [NotNull]
        private JoinPlayerResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(RegisterPlayerResult != null);
            Contract.Requires<InvalidOperationException>(AddComponentResults != null);
            Contract.Requires<InvalidOperationException>(SupplyCardResults != null);
            Contract.Requires<InvalidOperationException>(Generating != null);

            return new SucceedResult(
                InitialStatus,
                RegisterPlayerResult,
                AddComponentResults,
                SupplyCardResults,
                Generating
            );
        }
    }
}
