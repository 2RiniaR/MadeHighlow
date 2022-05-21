using System;

namespace RineaR.MadeHighlow.Actions.Valid.General.UpdateTurn
{
    public record UpdateTurnAction : ValidAction<UpdateTurnResult>
    {
        protected override UpdateTurnResult EvaluateBody(IHistory history)
        {
            throw new NotImplementedException();
        }
    }
}
