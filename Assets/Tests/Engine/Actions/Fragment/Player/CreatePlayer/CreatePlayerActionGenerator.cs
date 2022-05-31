namespace RineaR.MadeHighlow.Actions.CreatePlayer
{
    public static class CreatePlayerActionGenerator
    {
        public static CreatePlayerAction Empty => new(PlayerGenerator.Empty);
    }
}
