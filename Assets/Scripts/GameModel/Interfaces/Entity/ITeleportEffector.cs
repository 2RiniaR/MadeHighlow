using UnityEngine;

namespace RineaR.MadeHighlow.GameModel.Interfaces.Entity
{
    public interface ITeleportEffector
    {
        void OnTeleport(GameModel.Entity entity, ref Vector3Int? destination);
    }
}