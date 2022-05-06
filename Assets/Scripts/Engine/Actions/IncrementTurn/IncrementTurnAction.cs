namespace RineaR.MadeHighlow
{
    public record IncrementTurnAction : Action<IncrementTurnResult>
    {
        public override IncrementTurnResult Validate(in IActionContext context)
        {
            return new IncrementTurnResult();
        }
    }
}