namespace RineaR.MadeHighlow.Actions.IncrementTurn
{
    public static class Constants
    {
        public static World BeforeWorld { get; } = WorldGenerator.Empty with { CurrentTurn = new Turn(0) };
        public static World AfterWorld { get; } = WorldGenerator.Empty with { CurrentTurn = new Turn(1) };

        public static Result SucceedResult { get; } = new(new Turn(1));
    }
}
