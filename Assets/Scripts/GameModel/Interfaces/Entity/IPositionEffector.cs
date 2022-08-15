using UnityEngine;

namespace RineaR.MadeHighlow.GameModel.Interfaces.Entity
{
    public interface IPositionEffector
    {
        void OnPosition(GameModel.Entity entity, ref Vector3Int? position);
    }
}