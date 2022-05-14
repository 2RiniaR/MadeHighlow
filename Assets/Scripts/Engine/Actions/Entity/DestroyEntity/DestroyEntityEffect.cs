using RineaR.MadeHighlow.Actions.InstantDamage;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public abstract record DestroyEntityEffect
    {
        public static DestroyEntityEffect Reject => new RejectEffect();
    }
}
