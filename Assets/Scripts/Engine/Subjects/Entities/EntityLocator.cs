namespace RineaR.MadeHighlow
{
    public record EntityLocator
    {
        public ID<Entity> EntityID { get; init; } = ID<Entity>.None;
    }
}