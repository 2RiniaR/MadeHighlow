namespace RineaR.MadeHighlow.GameModel.Interfaces.Entity
{
    public interface IKillEffector
    {
        public void OnKill(Life life, ref float? probability);
    }
}