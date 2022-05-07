using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record JoinPlayerAction([NotNull] Player InitialPlayer) : Action<JoinPlayerResult>
    {
        public override JoinPlayerResult Validate(IActionContext context)
        {
            var currentContext = context;

            var registerResult = new RegisterPlayerAction(InitialPlayer.DeckSize).Validate(currentContext);
            currentContext = currentContext.Appended(registerResult);

            var supplyCardResults = new List<SupplyCardResult>();
            foreach (var card in InitialPlayer.Cards)
            {
                var result =
                    new SupplyCardAction(registerResult.RegisteredPlayer.PlayerID, card).Validate(currentContext);
                currentContext = currentContext.Appended(result);
                supplyCardResults.Add(result);
            }

            var addComponentResults = new List<AddComponentResult>();
            foreach (var component in InitialPlayer.Components)
            {
                var result =
                    new AddComponentAction(registerResult.RegisteredPlayer.PlayerID, component)
                        .Validate(currentContext);
                currentContext = currentContext.Appended(result);
                addComponentResults.Add(result);
            }

            throw new NotImplementedException();
        }
    }
}