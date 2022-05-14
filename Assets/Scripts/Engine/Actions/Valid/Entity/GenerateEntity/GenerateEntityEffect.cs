namespace RineaR.MadeHighlow.Actions.Valid.GenerateEntity
{
    public abstract record GenerateEntityEffect
    {
        public static GenerateEntityEffect Reject => new RejectEffect();
    }
}
