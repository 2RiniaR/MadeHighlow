namespace RineaR.MadeHighlow.GameModel.Interfaces.Entity
{
    public interface IHealEffector
    {
        public void OnHeal(Life life, ref int? damage);
    }
}