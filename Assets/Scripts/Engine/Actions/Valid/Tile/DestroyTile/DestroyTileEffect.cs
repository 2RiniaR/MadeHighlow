namespace RineaR.MadeHighlow.Actions.Valid.DestroyTile
{
    public abstract record DestroyTileEffect
    {
        public static DestroyTileEffect Reject => new RejectEffect();
    }
}
