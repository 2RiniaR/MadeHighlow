using UnityEngine;

namespace RineaR.MadeHighlow.GameModel.Interfaces.Entity
{
    public interface IStepEffector
    {
        void OnStep(GameModel.Entity entity, Vector2Int stepDirection, int availableCost);
    }
}