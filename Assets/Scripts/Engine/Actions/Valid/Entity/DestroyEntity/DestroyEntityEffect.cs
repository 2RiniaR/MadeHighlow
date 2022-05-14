namespace RineaR.MadeHighlow.Actions.Valid.DestroyEntity
{
    public abstract record DestroyEntityEffect
    {
        public static DestroyEntityEffect Reject => new RejectEffect();
    }
}
