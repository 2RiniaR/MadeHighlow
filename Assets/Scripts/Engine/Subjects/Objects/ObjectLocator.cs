namespace RineaR.MadeHighlow.Engine.Subjects.Objects
{
    public record ObjectLocator
    {
        public ID<Object> ObjectID { get; init; } = ID<Object>.None;
    }
}