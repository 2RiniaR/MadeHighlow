namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public abstract record GenerateEntityEffect
    {
        public static GenerateEntityEffect Reject => new RejectEffect();
    }
}
