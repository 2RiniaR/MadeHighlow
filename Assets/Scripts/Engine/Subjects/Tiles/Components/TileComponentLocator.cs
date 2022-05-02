namespace RineaR.MadeHighlow
{
    public record TileComponentLocator : TileLocator
    {
        public ID<TileComponent> ComponentID { get; init; } = ID<TileComponent>.None;
    }
}