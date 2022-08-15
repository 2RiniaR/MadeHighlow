using UnityEngine;

namespace RineaR.MadeHighlow.GameModel.Interfaces.Entity
{
    public interface IWalkEffector
    {
        void OnWalk(GameModel.Entity entity, Vector2Int[] route, int availableCost);
    }
}