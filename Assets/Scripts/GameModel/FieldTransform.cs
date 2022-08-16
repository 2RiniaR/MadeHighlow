using RineaR.MadeHighlow.GameModel.Geometry;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    [DisallowMultipleComponent]
    [ExecuteAlways]
    public class FieldTransform : MonoBehaviour
    {
        public enum PositionBindingMode
        {
            Lock,
            Input,
            Independent,
        }

        public FieldVector3 position;
        public FieldDirection3 direction;
        public Field field;
        public PositionBindingMode positionBindingMode;

        private void Reset()
        {
            RefreshReferences();
        }

        private void Start()
        {
            RefreshReferences();
        }

        private void Update()
        {
            if (field == null) return;
            switch (positionBindingMode)
            {
                case PositionBindingMode.Lock:
                    AdjustWorldPositionFromField();
                    break;
                case PositionBindingMode.Input:
                    AdjustFieldPositionFromWorld();
                    break;
            }
        }

        private void RefreshReferences()
        {
            field ??= GetComponentInParent<Field>();
        }

        private void AdjustWorldPositionFromField()
        {
            transform.position = field.FieldPositionToWorld(position);
        }

        private void AdjustFieldPositionFromWorld()
        {
            position = field.WorldPositionToField(transform.position);
        }
    }
}