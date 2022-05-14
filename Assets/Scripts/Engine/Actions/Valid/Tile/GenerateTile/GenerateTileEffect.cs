namespace RineaR.MadeHighlow.Actions.Valid.GenerateTile
{
    public abstract record GenerateTileEffect
    {
        public static GenerateTileEffect Reject => new RejectEffect();
    }
}
