using RineaR.MadeHighlow.Actions.CreatePlayer.RegisterPlayer;

namespace RineaR.MadeHighlow.Actions.RegisterPlayer
{
    public static class RegisterPlayerActionGenerator
    {
        public static Action Empty => new(ID.None, PlayerGenerator.Empty);
    }
}
