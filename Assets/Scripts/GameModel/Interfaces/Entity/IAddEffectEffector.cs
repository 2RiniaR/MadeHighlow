namespace RineaR.MadeHighlow.GameModel.Interfaces.Entity
{
    public interface IAddEffectEffector
    {
        void OnAddEffect(GameModel.Entity entity, ref EntityEffect effect);
    }
}