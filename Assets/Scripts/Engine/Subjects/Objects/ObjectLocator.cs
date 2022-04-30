namespace RineaR.MadeHighlow
{
    public record ObjectLocator
    {
        public ID<Object> ObjectID { get; init; } = ID<Object>.None;
    }
}