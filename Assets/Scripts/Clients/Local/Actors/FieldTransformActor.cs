using RineaR.MadeHighlow.GameModel;
using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local.Actors
{
    public class FieldTransformActor : MonoBehaviour
    {
        [Header("References on scene")]
        public FieldTransform role;

        private void Start()
        {
            SyncTransformForce();
        }

        public void SyncTransformForce()
        {
            transform.position = role.GetWorldPosition();
        }
    }
}