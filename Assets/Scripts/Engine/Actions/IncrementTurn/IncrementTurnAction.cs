namespace RineaR.MadeHighlow.Actions
{
    public record IncrementTurnAction() : Action(ActionType.IncrementTurn)
    {
        public IncrementTurnResult Run(in Session session)
        {
            return new IncrementTurnResult();
        }
    }
}