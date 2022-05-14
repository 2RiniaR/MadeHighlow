namespace RineaR.MadeHighlow.Actions.DestroyTile
{
    public abstract record DestroyTileEffect
    {
        public static DestroyTileEffect Reject => new RejectEffect();
    }
}
