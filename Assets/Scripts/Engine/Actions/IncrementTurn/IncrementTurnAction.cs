namespace RineaR.MadeHighlow
{
    public record IncrementTurnAction : Action<IncrementTurnResult>
    {
        public override IncrementTurnResult Validate(IActionContext context)
        {
            return new IncrementTurnResult();
        }
    }
}
