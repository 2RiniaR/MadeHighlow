using System.Collections.Generic;
using System.Linq;
using RineaR.MadeHighlow.GameModel.Geometry;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class Field : MonoBehaviour, IField
    {
        private List<FieldTransform> _objects = new();

        public FieldVector3 WorldPositionToField(Vector3 position)
        {
            var origin = transform.position;
            return new FieldVector3
            {
                horizontal = Mathf.FloorToInt(position.x - origin.x + 0.5f),
                vertical = Mathf.FloorToInt(position.z - origin.z + 0.5f),
                height = Mathf.FloorToInt(position.y - origin.y + 0.5f),
            };
        }

        public Vector3 FieldPositionToWorld(FieldVector3 fieldPosition)
        {
            var origin = transform.position;
            return new Vector3(
                fieldPosition.horizontal + origin.x,
                fieldPosition.height + origin.y,
                fieldPosition.vertical + origin.z
            );
        }

        private void CollectObjects()
        {
            _objects = GetComponentsInChildren<FieldTransform>().ToList();
            _objects.Sort((obj1, obj2) =>
            {
                var horizontalCompare = obj1.position.horizontal.CompareTo(obj2.position.horizontal);
                if (horizontalCompare != 0) return horizontalCompare;
                var verticalCompare = obj1.position.vertical.CompareTo(obj2.position.vertical);
                if (verticalCompare != 0) return verticalCompare;
                return obj1.position.height.CompareTo(obj2.position.height);
            });
            foreach (var obj in _objects) obj.transform.SetSiblingIndex(_objects.Count - 1);
        }

        [ContextMenu("Unlock all objects")]
        public void UnlockObjects()
        {
            CollectObjects();
            foreach (var fieldTransform in _objects)
                fieldTransform.positionBindingMode = FieldTransform.PositionBindingMode.Input;
        }

        [ContextMenu("Lock all objects")]
        public void LockObjects()
        {
            CollectObjects();
            foreach (var fieldTransform in _objects)
                fieldTransform.positionBindingMode = FieldTransform.PositionBindingMode.Lock;
        }
    }
}