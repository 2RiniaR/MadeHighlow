using UnityEngine;

namespace RineaR.MadeHighlow.GameModel.Geometry
{
    public class FieldGeometryDebugger : MonoBehaviour
    {
        public FieldDirection2 direction2;
        public FieldDirection3 direction3;
        public FieldVector2 vector2;
        public FieldVector3 vector3;

        private void Start()
        {
            Debug.Log("" +
                      $"FieldDirection2: {direction2}\n" +
                      $"FieldDirection3: {direction3}\n" +
                      $"FieldVector2: {vector2}\n" +
                      $"FieldVector3: {vector3}\n"
            );
        }
    }
}