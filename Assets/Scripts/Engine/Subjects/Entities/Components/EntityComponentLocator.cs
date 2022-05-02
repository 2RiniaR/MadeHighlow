namespace RineaR.MadeHighlow
{
    public record EntityComponentLocator : EntityLocator
    {
        public ID<EntityComponent> ComponentID { get; init; } = ID<EntityComponent>.None;
    }
}