using UnityEngine;

namespace RineaR.MadeHighlow.GameModel.Interfaces.Entity
{
    public interface IKnockBackEffector
    {
        void OnKnockBack(GameModel.Entity entity, Vector3Int[] route);
    }
}