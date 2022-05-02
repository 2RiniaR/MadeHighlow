namespace RineaR.MadeHighlow
{
    public record TileLocator
    {
        public ID<Tile> TileID { get; init; } = ID<Tile>.None;
    }
}