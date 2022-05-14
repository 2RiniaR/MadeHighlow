namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public abstract record GenerateTileEffect
    {
        public static GenerateTileEffect Reject => new RejectEffect();
    }
}
