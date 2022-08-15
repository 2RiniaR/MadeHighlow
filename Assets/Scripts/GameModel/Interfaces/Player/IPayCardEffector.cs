namespace RineaR.MadeHighlow.GameModel.Interfaces.Player
{
    public interface IPayCardEffector
    {
        void OnPayCard(GameModel.Player player, ref Card card);
    }
}