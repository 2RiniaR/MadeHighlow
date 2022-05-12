using System.Collections.Generic;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AddComponent;
using RineaR.MadeHighlow.Actions.JoinPlayer.RegisterPlayer;
using RineaR.MadeHighlow.Actions.SupplyCard;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public record JoinPlayerAction([NotNull] Player InitialPlayer) : Action<JoinPlayerResult>
    {
        public override JoinPlayerResult Evaluate(IActionContext context)
        {
            var currentContext = context;

            var registerPlayerResult = RegisterNewPlayer(ref currentContext);
            var playerID = registerPlayerResult.RegisteredPlayer.PlayerID;
            var addComponentResults = InitializeComponents(ref currentContext, playerID);
            var supplyCardResults = InitializeCards(ref currentContext, playerID);

            return new SucceedResult(registerPlayerResult, addComponentResults, supplyCardResults);
        }

        [NotNull]
        private RegisterPlayer.SucceedResult RegisterNewPlayer([NotNull] ref IActionContext currentContext)
        {
            var registerPlayerResult = new RegisterPlayerAction(InitialPlayer).Evaluate(currentContext);
            currentContext = currentContext.Appended(registerPlayerResult);
            return registerPlayerResult;
        }

        [NotNull]
        [ItemNotNull]
        private ValueList<AddComponentResult> InitializeComponents(
            [NotNull] ref IActionContext currentContext,
            [NotNull] PlayerID playerID
        )
        {
            var addComponentResults = new List<AddComponentResult>();
            foreach (var component in InitialPlayer.Components)
            {
                var result = new AddComponentAction(playerID, component).Evaluate(currentContext);
                currentContext = currentContext.Appended(result);
                addComponentResults.Add(result);
            }

            return addComponentResults.ToValueList();
        }

        [NotNull]
        [ItemNotNull]
        private ValueList<SupplyCardResult> InitializeCards(
            [NotNull] ref IActionContext currentContext,
            [NotNull] PlayerID playerID
        )
        {
            var supplyCardResults = new List<SupplyCardResult>();
            foreach (var card in InitialPlayer.Cards)
            {
                var result = new SupplyCardAction(card with { OwnerPlayerID = playerID }).Evaluate(currentContext);
                currentContext = currentContext.Appended(result);
                supplyCardResults.Add(result);
            }

            return supplyCardResults.ToValueList();
        }
    }
}
