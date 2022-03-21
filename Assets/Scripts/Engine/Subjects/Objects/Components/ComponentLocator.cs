namespace RineaR.MadeHighlow.Engine.Subjects.Objects.Components
{
    public record ComponentLocator : ObjectLocator
    {
        public ID<Component> ComponentID { get; init; } = ID<Component>.None;
    }
}