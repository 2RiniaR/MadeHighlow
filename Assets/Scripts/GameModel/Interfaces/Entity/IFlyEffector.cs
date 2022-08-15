using UnityEngine;

namespace RineaR.MadeHighlow.GameModel.Interfaces.Entity
{
    public interface IFlyEffector
    {
        void OnFly(GameModel.Entity entity, Vector3Int[] route);
    }
}