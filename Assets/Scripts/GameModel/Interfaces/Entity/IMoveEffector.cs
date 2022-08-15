using UnityEngine;

namespace RineaR.MadeHighlow.GameModel.Interfaces.Entity
{
    public interface IMoveEffector
    {
        void OnMove(GameModel.Entity entity, ref Vector3Int? direction);
    }
}