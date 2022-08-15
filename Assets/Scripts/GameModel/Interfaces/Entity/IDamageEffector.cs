namespace RineaR.MadeHighlow.GameModel.Interfaces.Entity
{
    public interface IDamageEffector
    {
        public void OnDamage(Life life, ref int? damage);
    }
}