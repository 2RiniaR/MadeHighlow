namespace RineaR.MadeHighlow.Actions
{
    public record EmptyResult() : Result(ActionType.Empty)
    {
        public override World Simulate(in World world)
        {
            return world;
        }
    }
}