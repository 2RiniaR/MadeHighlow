namespace RineaR.MadeHighlow.GameModel.Interfaces.Entity
{
    public interface IRemoveEffectEffector
    {
        void OnRemoveEffect(GameModel.Entity entity, ref EntityEffect effect);
    }
}