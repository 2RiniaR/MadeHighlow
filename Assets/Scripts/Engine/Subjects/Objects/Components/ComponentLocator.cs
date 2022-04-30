namespace RineaR.MadeHighlow
{
    public record ComponentLocator : ObjectLocator
    {
        public ID<Component> ComponentID { get; init; } = ID<Component>.None;
    }
}