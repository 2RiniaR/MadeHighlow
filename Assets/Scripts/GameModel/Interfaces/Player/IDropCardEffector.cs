namespace RineaR.MadeHighlow.GameModel.Interfaces.Player
{
    public interface IDropCardEffector
    {
        void OnDropCard(GameModel.Player player, ref Card card);
    }
}