namespace RineaR.MadeHighlow
{
    public static class WorldGenerator
    {
        public static World Empty => new(
            ID.From(1),
            ValueList<Player>.Empty,
            ValueList<Tile>.Empty,
            ValueList<Entity>.Empty,
            new Turn(0),
            ValueList<Command>.Empty
        );
    }
}
