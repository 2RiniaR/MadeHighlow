namespace RineaR.MadeHighlow.GameModel.Interfaces
{
    public interface ITurnUpdater
    {
        int UpdateTurnPriority => 0;
        void UpdateTurn();
    }
}