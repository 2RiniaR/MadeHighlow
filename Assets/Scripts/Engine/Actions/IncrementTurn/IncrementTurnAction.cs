namespace RineaR.MadeHighlow.Actions.IncrementTurn
{
    public record IncrementTurnAction() : Action(ActionType.IncrementTurn)
    {
        public IncrementTurnEvent Run(in Session session)
        {
            return new IncrementTurnEvent();
        }
    }
}