namespace RineaR.MadeHighlow.GameModel.Interfaces.Player
{
    public interface ISupplyCardEffector
    {
        void OnSupplyCard(GameModel.Player player, ref Card card);
    }
}