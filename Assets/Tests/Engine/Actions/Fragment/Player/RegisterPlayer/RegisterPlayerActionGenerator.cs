namespace RineaR.MadeHighlow.Actions.RegisterPlayer
{
    public static class RegisterPlayerActionGenerator
    {
        public static RegisterPlayerAction Empty => new(ID.None, PlayerGenerator.Empty);
    }
}
