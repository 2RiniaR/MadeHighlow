namespace RineaR.MadeHighlow.Actions.AllocateID
{
    public static class Constants
    {
        public static World BeforeWorld { get; } = WorldGenerator.Empty with { NextID = ID.From(1) };
        public static World AfterWorld { get; } = WorldGenerator.Empty with { NextID = ID.From(2) };

        public static Result SucceedResult { get; } = new(ID.From(1));
    }
}
